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
    
    // 기존 문제와 답안, 정보가 들어있는 TextMeshPro Text
    public TMP_Text inputText;

    // 문제와 선택지 및 결과를 표시할 TextMeshPro 요소들
    public TMP_Text problemText;        // 문제를 표시할 TMP
    public TMP_Text resultText;         // 정답/오답 결과를 표시할 TMP

    // 4개의 버튼 (선택지) 및 관련 텍스트
    public Button choiceButton1;
    public Button choiceButton2;
    public Button choiceButton3;
    public Button choiceButton4;

    public TMP_Text choiceText1;
    public TMP_Text choiceText2;
    public TMP_Text choiceText3;
    public TMP_Text choiceText4;

    // 패널 두 개
    public GameObject Detail_Panel;  // 비활성화할 패널 (기존 입력 문제)
    public GameObject Similar_Quiz_Panel;  // 활성화할 패널 (결과 표시 패널)

    // 문제 생성 요청을 보내는 버튼
    public Button generateProblemButton;  

    // 선택된 정답을 저장할 변수
    private string correctAnswer;
    public Button backButton;
    public GameObject result_panel;

    // Start에서 버튼 클릭 이벤트 연결
    void Start()
    {
        // 버튼 클릭 이벤트 설정
        generateProblemButton.onClick.AddListener(OnGenerateProblemButtonClick);
        backButton.onClick.AddListener(Back);

        // 각 선택지 버튼에 이벤트 리스너 추가
        choiceButton1.onClick.AddListener(() => CheckAnswer(choiceText1.text));
        choiceButton2.onClick.AddListener(() => CheckAnswer(choiceText2.text));
        choiceButton3.onClick.AddListener(() => CheckAnswer(choiceText3.text));
        choiceButton4.onClick.AddListener(() => CheckAnswer(choiceText4.text));
        
        Similar_Quiz_Panel.SetActive(false);  // 결과 패널은 처음에 비활성화
    }

    // 문제 생성 버튼 클릭 시 실행되는 함수
    void OnGenerateProblemButtonClick()
    {
        // 기존 문제 텍스트 가져오기
        string originalProblem = inputText.text;

        // 패널 전환: 입력 패널 비활성화, 결과 패널 활성화
        Detail_Panel.SetActive(false);
        Similar_Quiz_Panel.SetActive(true);

        // GPT API로 문제 생성 요청 전송
        StartCoroutine(SendProblemGenerationRequest(originalProblem));
    }

    void Back()
    {
        // Reactivate the Detail Panel and deactivate the Similar Quiz Panel
        Detail_Panel.SetActive(true);
        Similar_Quiz_Panel.SetActive(false);
        result_panel.SetActive(false);

        // Reset the problem text and result text
        problemText.text = "";
        resultText.text = "";

        // Reset the choice buttons' text
        choiceText1.text = "";
        choiceText2.text = "";
        choiceText3.text = "";
        choiceText4.text = "";

        // Optionally, reset the correct answer
        correctAnswer = "";

        // Reset any other variables or states that should be initialized
        // For example, if you have some state flags or counters, reset them here
    }

    // GPT API로 요청을 보내는 Coroutine 함수
    private IEnumerator SendProblemGenerationRequest(string originalProblem)
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions";

        // JSON 데이터를 작성할 때 문자열 처리를 수정
        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant that creates 4-choice multiple-choice questions." },
                new { role = "user", content = $@"
                    다음은 주어진 문제입니다:

                    {originalProblem}

                    이 문제와 유사한 단 하나의 4지선다형 객관식 문제를 생성하고, 각 선택지를 반드시 1., 2., 3., 4. 순서로 표시해 주세요.

                    형식:
                    문제 설명: (무조건 바로 옆에 문제 나오기)
                    4개의 선택지 (각각 1., 2., 3., 4.로 시작)
                    정답: (선택지 중 하나)"
                }
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

            // 요청 보내기
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("GPT API 응답: " + responseText);  // 응답 전체 확인용 디버그 로그

                // 응답 데이터를 파싱
                var gptResponse = JsonConvert.DeserializeObject<GPTProblemResponse>(responseText);

                // 응답 데이터에서 문제와 선택지 및 정답 설정
                if (gptResponse != null && gptResponse.choices != null && gptResponse.choices.Length > 0)
                {
                    var problemMessage = gptResponse.choices[0].message.content;
                    Debug.Log("GPT 응답에서 파싱된 문제 내용: " + problemMessage);  // 파싱된 내용 확인 디버깅
                    ParseAndDisplayProblem(problemMessage);
                }
            }
            else
            {
                resultText.text = "요청 실패: " + request.error;
                Debug.LogError("요청 실패: " + request.error);
            }
        }
    }

// GPT의 응답을 파싱하고 문제와 선택지를 설정하는 함수
private void ParseAndDisplayProblem(string problemMessage)
{
    // 문제와 선택지 및 정답을 파싱 (각 줄을 기준으로 분리)
    string[] lines = problemMessage.Split('\n');
    
    // 첫 번째 줄이 "문제 설명:"일 수 있으므로 해당 부분을 제거하고 문제만 표시
    if (lines[0].StartsWith("문제 설명:"))
    {
        problemText.text = lines[0].Substring(6).Trim();  // "문제 설명:" 제거 후 텍스트만 표시
    }
    else
    {
        problemText.text = lines[0];  // 다른 경우는 그냥 첫 번째 줄을 문제로 설정
    }

    // 선택지 번호에 따라 텍스트를 설정
    foreach (string line in lines)
    {
        if (line.StartsWith("1. "))
            choiceText1.text = line.Substring(3);  // "1. " 이후의 텍스트
        else if (line.StartsWith("2. "))
            choiceText2.text = line.Substring(3);  // "2. " 이후의 텍스트
        else if (line.StartsWith("3. "))
            choiceText3.text = line.Substring(3);  // "3. " 이후의 텍스트
        else if (line.StartsWith("4. "))
            choiceText4.text = line.Substring(3);  // "4. " 이후의 텍스트
    }

    // 정답 저장 (정답은 "정답: "으로 시작하는 줄에서 선택지 번호를 제거하고 텍스트만 저장)
    foreach (string line in lines)
    {
        if (line.StartsWith("정답: "))
        {
            string correctLine = line.Substring(4).Trim();  // "정답: " 이후의 텍스트 (Trim으로 공백 제거)
            
            // 선택지 번호(예: "2. ")를 제거하고, 정답 텍스트만 저장
            if (correctLine.Length > 3 && (correctLine.StartsWith("1. ") || correctLine.StartsWith("2. ") ||
                                           correctLine.StartsWith("3. ") || correctLine.StartsWith("4. ")))
            {
                correctAnswer = correctLine.Substring(3).Trim();  // "2. " 이후의 텍스트를 정답으로 저장
                Debug.Log("정답: " + correctAnswer);  // 정답 확인용 디버그 로그
            }
            else
            {
                correctAnswer = correctLine;  // 선택지 번호 없이 바로 정답 텍스트 저장
                Debug.Log("정답: " + correctAnswer);  // 정답 확인용 디버그 로그
            }
        }
    }
}

    // 선택지가 눌렸을 때 정답 여부를 확인하는 함수
    private void CheckAnswer(string selectedAnswer)
    {
        Debug.Log(selectedAnswer);
        result_panel.SetActive(true);
        if (selectedAnswer == correctAnswer)
        {
            resultText.text = "정답입니다!";
        }
        else
        {
            resultText.text = "오답입니다. 정답은 " + correctAnswer;
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