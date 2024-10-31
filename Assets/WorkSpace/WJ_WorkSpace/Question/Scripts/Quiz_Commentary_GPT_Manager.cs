using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json;

public class Quiz_Commentary_GPT_Manager : MonoBehaviour
{
    private string apiKey = "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";

    public TMP_Text questionText;  
    public TMP_Text resultText;    
    public Button sendButton;      
    public Button backButton;      // Back button

    public GameObject Detail_Panel;      
    public GameObject Quiz_commentary_Panel;     

    private string initialResultText;

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);
        backButton.onClick.AddListener(Back); // Connect the Back button
        Quiz_commentary_Panel.SetActive(false);  
        
        initialResultText = resultText.text; 
    }

    void OnSendButtonClick()
    {
        string formattedText = questionText.text;

        Detail_Panel.SetActive(false);
        Quiz_commentary_Panel.SetActive(true);

        StartCoroutine(SendGPTExplanationRequest(formattedText));
    }

    void Back()
    {
        // Activate the Detail_Panel and deactivate the Quiz_commentary_Panel
        Detail_Panel.SetActive(true);
        Quiz_commentary_Panel.SetActive(false);
    }

    void Update()
    {
        if (!Quiz_commentary_Panel.activeSelf)
        {
            resultText.text = initialResultText; 
        }
    }

    private IEnumerator SendGPTExplanationRequest(string formattedText)
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions"; 

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant that explains concepts." },
                new { role = "user", content = formattedText }
            },
            max_tokens = 300,
            temperature = 0.7
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
            request.SetRequestHeader("Content-Type", "application/json");

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                var gptResponse = JsonConvert.DeserializeObject<GPTExplanationResponse>(responseText);

                resultText.text = gptResponse.choices[0].message.content.Trim();
            }
            else
            {
                resultText.text = "요청 실패: " + request.error;
            }
        }
    }
}

[System.Serializable]
public class GPTExplanationResponse
{
    public GPTExplanationChoice[] choices;
}

[System.Serializable]
public class GPTExplanationChoice
{
    public GPTMessage message;
}

[System.Serializable]
public class GPTMessage
{
    public string content;
}
