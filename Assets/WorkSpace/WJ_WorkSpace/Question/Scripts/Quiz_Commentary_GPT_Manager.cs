using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json;


public class Quiz_Commentary_GPT_Manager : MonoBehaviour
{
    // GPT API 키 설정
    private string apiKey = "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";
    
    // UI 요소들 (TextMeshPro Text와 Button)
    public TMP_Text questionText;  // 미리 설정된 질문이 있는 TextMeshPro Text
    public TMP_Text resultText;    // GPT 응답을 출력할 TextMeshPro Text
    public Button sendButton;      // 요청을 보내는 버튼

    // 패널 두 개 추가
    public GameObject panelA;      // 비활성화할 패널
    public GameObject panelB;      // 활성화할 패널 (결과 표시용)

    // Start에서 버튼 클릭 이벤트 연결
    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);
        panelB.SetActive(false);  // B 패널은 처음에 비활성화
    }

    // 버튼 클릭 시 실행되는 함수
    void OnSendButtonClick()
    {
        // 미리 설정된 TextMeshPro 텍스트에서 질문 형식 가져오기
        string formattedText = questionText.text;

        // 패널 전환: A 패널 비활성화, B 패널 활성화
        panelA.SetActive(false);
        panelB.SetActive(true);

        // GPT API로 질문 전송
        StartCoroutine(SendGPTExplanationRequest(formattedText));
    }

    // GPT API로 요청을 보내는 Coroutine 함수 (Chat Completion 방식)
    private IEnumerator SendGPTExplanationRequest(string formattedText)
    {
        // GPT-3.5-turbo 엔드포인트
        string apiUrl = "https://api.openai.com/v1/chat/completions";  // Chat Completion 엔드포인트

        // Chat Completion 방식의 프롬프트 구성
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

                // GPT 응답을 UI TextMeshPro Text에 출력
                resultText.text = gptResponse.choices[0].message.content.Trim();
            }
            else
            {
                resultText.text = "요청 실패: " + request.error;
            }
        }
    }
}

// GPT API 응답 데이터 형식
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
