using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json;

public class Similar_Quiz_Maker_Manager : MonoBehaviour
{
    private string apiKey = "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";  // OpenAI API 키 설정
    
    // UI 요소들 (TextMeshPro Text와 Button)
    public TMP_Text inputText;  // 기존 문제와 답안, 정보가 들어있는 TextMeshPro Text
    public TMP_Text generatedProblemText;  // 생성된 문제를 표시할 TextMeshPro Text
    public Button generateProblemButton;  // 문제 생성 요청을 보내는 버튼

    // 패널 두 개 추가
    public GameObject inputPanel;  // 비활성화할 패널 (기존 입력 문제)
    public GameObject resultPanel;  // 활성화할 패널 (결과 표시 패널)

    // Start에서 버튼 클릭 이벤트 연결
    void Start()
    {
        generateProblemButton.onClick.AddListener(OnGenerateProblemButtonClick);
        resultPanel.SetActive(false);  // 결과 패널은 처음에 비활성화
    }

    // 버튼 클릭 시 실행되는 함수
    void OnGenerateProblemButtonClick()
    {
        // 미리 설정된 TextMeshPro 텍스트에서 입력 문제 형식 가져오기
        string originalProblem = inputText.text;

        // 패널 전환: 입력 패널 비활성화, 결과 패널 활성화
        inputPanel.SetActive(false);
        resultPanel.SetActive(true);

        // GPT API로 문제 생성 요청 전송
        StartCoroutine(SendProblemGenerationRequest(originalProblem));
    }

    // GPT API로 요청을 보내는 Coroutine 함수 (문제 생성 요청)
    private IEnumerator SendProblemGenerationRequest(string originalProblem)
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions";  // Chat Completion 엔드포인트 사용

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant that creates similar questions." },
                new { role = "user", content = $@"
                    다음은 주어진 문제입니다:

                    {originalProblem}

                    위 문제와 유사한 새로운 문제를 만들어주세요."
                }
            },
            max_tokens = 150,
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

            // 요청 보내기
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;

                // 응답 확인을 위한 디버그 로그 출력
                Debug.Log("응답 데이터: " + responseText);

                // 응답 데이터를 파싱하기 전 응답이 제대로 오는지 확인
                var gptResponse = JsonConvert.DeserializeObject<GPTProblemResponse>(responseText);

                // 응답 데이터가 null인지 확인
                if (gptResponse != null && gptResponse.choices != null && gptResponse.choices.Length > 0)
                {
                    var problemMessage = gptResponse.choices[0].message;
                    if (problemMessage != null)
                    {
                        generatedProblemText.text = "생성된 문제: " + problemMessage.content.Trim();
                    }
                    else
                    {
                        generatedProblemText.text = "생성된 문제를 찾을 수 없습니다.";
                        Debug.LogWarning("problemMessage가 null입니다.");
                    }
                }
                else
                {
                    generatedProblemText.text = "응답에서 문제를 찾을 수 없습니다.";
                    Debug.LogWarning("gptResponse 또는 gptResponse.choices가 null이거나 비어 있습니다.");
                }
            }
            else
            {
                // 요청 실패 처리
                generatedProblemText.text = "요청 실패: " + request.error;
                Debug.LogError("요청 실패: " + request.error);
            }
        }
    }
}

// GPT API 응답 데이터 형식 (문제 생성용)
[System.Serializable]
public class GPTProblemResponse
{
    public GPTProblemChoice[] choices;
}

[System.Serializable]
public class GPTProblemChoice
{
    public GPTProblemMessage message;
}

[System.Serializable]
public class GPTProblemMessage
{
    public string content;
}