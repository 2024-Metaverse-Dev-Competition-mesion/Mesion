using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

[System.Serializable]
public class UserInfo
{
    public string userName;
    public int age;
    public List<string> interests;

    public UserInfo(string userName, int age, List<string> interests)
    {
        this.userName = userName;
        this.age = age;
        this.interests = interests;
    }

    public static UserInfo LoadUserInfo(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<UserInfo>(json);
        }
        else
        {
            Debug.LogError("User info file not found: " + filePath);
            return null;
        }
    }

    public static void SaveSampleUserInfo(string filePath)
    {
        UserInfo sampleUser = new UserInfo("Alice", 25, new List<string> { "art", "music", "traveling" });
        string json = JsonConvert.SerializeObject(sampleUser, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log("Sample user info file created at: " + filePath);
    }
}

public class GPTChatbot : MonoBehaviour
{
    private string apiKey = "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";
    private string apiUrl = "https://api.openai.com/v1/chat/completions";
    private string summaryApiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";

    [SerializeField]
    private GPTChatbotUI uiHandler;

    private UserInfo userInfo;
    private string userInfoFilePath;

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class GPTRequest
    {
        public string model;
        public List<Message> messages;
        public int max_tokens;
        public int n;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }

    [System.Serializable]
    public class GPTResponse
    {
        public List<Choice> choices;
    }

    private List<Message> conversationHistory = new List<Message>();
    private string conversationFilePath;

    private const int maxConversationLength = 10;

    private void Awake()
    {
        conversationFilePath = Path.Combine(Application.persistentDataPath, "conversationHistory.json");
        userInfoFilePath = Path.Combine(Application.persistentDataPath, "userInfo.json");
        Debug.Log("Conversation file path: " + conversationFilePath);
        Debug.Log("User info file path: " + userInfoFilePath);
    }

    private void Start()
    {
        LoadConversationHistory();

        if (!File.Exists(userInfoFilePath))
        {
            UserInfo.SaveSampleUserInfo(userInfoFilePath);
        }

        userInfo = UserInfo.LoadUserInfo(userInfoFilePath);

        if (userInfo == null)
        {
            // Use default user info if loading fails
            userInfo = new UserInfo("John Doe", 30, new List<string> { "coding", "gaming", "reading" });
        }

        Debug.Log($"Loaded user info: {userInfo.userName}, {userInfo.age}, {string.Join(", ", userInfo.interests)}");
    }

    private void SaveConversationHistory()
    {
        string json = JsonConvert.SerializeObject(conversationHistory);
        File.WriteAllText(conversationFilePath, json);
    }

    private void LoadConversationHistory()
    {
        if (File.Exists(conversationFilePath))
        {
            string json = File.ReadAllText(conversationFilePath);
            conversationHistory = JsonConvert.DeserializeObject<List<Message>>(json);
        }
    }

    private IEnumerator SummarizeMessage(string content, System.Action<string> callback)
    {
        string prompt = "Summarize the following conversation:\n\n" + content;
        var request = new GPTRequest
        {
            model = "text-davinci-002",
            messages = new List<Message> { new Message { role = "user", content = prompt } },
            max_tokens = 150,
            n = 1
        };

        string jsonRequest = JsonConvert.SerializeObject(request);
        UnityWebRequest summarizeRequest = new UnityWebRequest(summaryApiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
        summarizeRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        summarizeRequest.downloadHandler = new DownloadHandlerBuffer();
        summarizeRequest.SetRequestHeader("Content-Type", "application/json");
        summarizeRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return summarizeRequest.SendWebRequest();

        if (summarizeRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + summarizeRequest.error);
        }
        else
        {
            string jsonResponse = summarizeRequest.downloadHandler.text;
            GPTResponse response = JsonConvert.DeserializeObject<GPTResponse>(jsonResponse);
            string summary = response.choices[0]?.message?.content;

            if (string.IsNullOrEmpty(summary))
            {
                Debug.LogError("Summary response is empty");
            }
            else
            {
                callback(summary);
            }
        }
    }

    private void ManageConversationHistory()
    {
        if (conversationHistory.Count > maxConversationLength)
        {
            string contentToSummarize = string.Join("\n", conversationHistory.Select(m => m.content).ToArray());
            StartCoroutine(SummarizeMessage(contentToSummarize, summary =>
            {
                conversationHistory.Clear();
                conversationHistory.Add(new Message { role = "system", content = summary });
                SaveConversationHistory();
            }));
        }
    }

    public void SendInitialRequest(string prompt)
    {
        if (uiHandler == null)
        {
            Debug.LogError("UI Handler is not assigned in the Inspector.");
            return;
        }

        conversationHistory.Clear();
        conversationHistory.Add(new Message { role = "system", content = $"The user's name is {userInfo.userName}, age is {userInfo.age}, and their interests are {string.Join(", ", userInfo.interests)}." });
        conversationHistory.Add(new Message { role = "user", content = prompt });

        GPTRequest request = new GPTRequest
        {
            model = "gpt-3.5-turbo",
            messages = new List<Message>(conversationHistory),
            max_tokens = 300,
            n = 1
        };

        string jsonRequest = JsonConvert.SerializeObject(request);
        StartCoroutine(PostRequest(apiUrl, jsonRequest, true));
    }

    public void SendFollowUpRequest(string userInput)
    {
        conversationHistory.Add(new Message { role = "user", content = userInput });
        ManageConversationHistory();

        GPTRequest request = new GPTRequest
        {
            model = "gpt-3.5-turbo",
            messages = new List<Message>(conversationHistory),
            max_tokens = 200,
            n = 1
        };

        string jsonRequest = JsonConvert.SerializeObject(request);
        StartCoroutine(PostRequest(apiUrl, jsonRequest, false));
    }

    IEnumerator PostRequest(string url, string json, bool isInitialRequest)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            if (string.IsNullOrEmpty(jsonResponse))
            {
                Debug.LogError("Response is empty");
                yield break;
            }

            GPTResponse response = JsonConvert.DeserializeObject<GPTResponse>(jsonResponse);

            if (response == null || response.choices == null || response.choices.Count == 0)
            {
                Debug.LogError("Invalid response");
                yield break;
            }

            string gptResponseText = response.choices[0]?.message?.content;

            if (gptResponseText == null)
            {
                Debug.LogError("Response text is null");
            }
            else
            {
                conversationHistory.Add(new Message { role = "assistant", content = gptResponseText });
                ManageConversationHistory();
                SaveConversationHistory();
                uiHandler.DisplayGPTResponse(gptResponseText);

                SendFollowUpRequestForChoices(gptResponseText);
            }
        }
    }

    public void SendFollowUpRequestForChoices(string gptResponse)
    {
        List<Message> messages = new List<Message>
        {
            new Message { role = "system", content = $"You are a helpful assistant. The user's name is {userInfo.userName}, age is {userInfo.age}, and their interests are {string.Join(", ", userInfo.interests)}. Provide four possible user responses to the following assistant response." },
            new Message { role = "assistant", content = gptResponse }
        };

        GPTRequest request = new GPTRequest
        {
            model = "gpt-3.5-turbo",
            messages = messages,
            max_tokens = 300,
            n = 1
        };

        string jsonRequest = JsonConvert.SerializeObject(request);
        StartCoroutine(PostRequestForChoices(apiUrl, jsonRequest));
    }

    IEnumerator PostRequestForChoices(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            if (string.IsNullOrEmpty(jsonResponse))
            {
                Debug.LogError("Response is empty");
                yield break;
            }

            GPTResponse response = JsonConvert.DeserializeObject<GPTResponse>(jsonResponse);

            if (response == null || response.choices == null || response.choices.Count == 0)
            {
                Debug.LogError("Invalid response");
                yield break;
            }

            string gptResponseText = response.choices[0]?.message?.content;

            if (gptResponseText == null)
            {
                Debug.LogError("Response text is null");
            }
            else
            {
                string[] choices = ParseChoices(gptResponseText);
                uiHandler.DisplayChoices(choices);
            }
        }
    }

    private string[] ParseChoices(string rawResponse)
    {
        string[] separators = new string[] { "1.", "2.", "3.", "4." };
        List<string> choices = new List<string>();

        for (int i = 0; i < separators.Length; i++)
        {
            int startIndex = rawResponse.IndexOf(separators[i]);
            int endIndex = (i + 1 < separators.Length) ? rawResponse.IndexOf(separators[i + 1]) : rawResponse.Length;

            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
            {
                string choice = rawResponse.Substring(startIndex, endIndex - startIndex).Trim();
                if (!string.IsNullOrEmpty(choice))
                {
                    choices.Add(choice);
                }
            }
        }

        return choices.ToArray();
    }
}