using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; // Button을 위해 필요
using TMPro;  // TextMeshPro 네임스페이스 사용

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;    // 질문 텍스트 UI
    public TextMeshProUGUI resultText;      // 결과 텍스트 UI
    public Button[] optionsButtons;         // 선택지 버튼 배열
    public QuizData quizData;               // 퀴즈 데이터 (ScriptableObject)

    private int currentQuestionIndex = 0;   // 현재 질문 인덱스
    private int score = 0;                  // 점수
    public int currentPart = 0;             // 현재 파트 (0: Part 1, 1: Part 2, 2: Part 3)
    private List<Question> currentPartQuestions; // 현재 파트의 질문 리스트

    void Start()
    {
        LoadQuestionsForPart(currentPart);
        DisplayQuestion();
    }

    // 현재 파트에 해당하는 질문들만 로드하고 랜덤하게 섞은 후, 5개만 선택
    void LoadQuestionsForPart(int part)
    {
        currentPartQuestions = quizData.questions.Where(q => q.part == part).ToList();
        
        if (currentPartQuestions.Count == 0)
        {
            Debug.LogWarning("선택된 파트에 질문이 없습니다.");
            EndQuiz();
        }
        else
        {
            // 질문을 랜덤하게 섞음
            System.Random rnd = new System.Random();
            currentPartQuestions = currentPartQuestions.OrderBy(q => rnd.Next()).Take(10).ToList(); // 10개만 선택
        }
    }

    // 질문과 선택지를 화면에 표시
    public void DisplayQuestion()
    {
        if (currentPartQuestions == null || currentPartQuestions.Count == 0)
        {
            resultText.text = "해당 파트에 질문이 없습니다.";
            return;
        }

        if (currentQuestionIndex >= currentPartQuestions.Count)
        {
            EndQuiz();
            return;
        }

        questionText.text = currentPartQuestions[currentQuestionIndex].questionText;

        for (int i = 0; i < optionsButtons.Length; i++)
        {
            if (i < currentPartQuestions[currentQuestionIndex].options.Length)
            {
                optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentPartQuestions[currentQuestionIndex].options[i];
                optionsButtons[i].onClick.RemoveAllListeners(); // 이전 리스너 제거
                int index = i;
                optionsButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                optionsButtons[i].gameObject.SetActive(false); // 선택지가 없는 버튼은 비활성화
            }
        }
    }

    // 사용자가 답변을 선택했을 때 호출
    public void OnAnswerSelected(int index)
    {
        if (index < 0 || index >= optionsButtons.Length)
        {
            Debug.LogError("잘못된 선택지 인덱스");
            return;
        }

        if (index == currentPartQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < currentPartQuestions.Count)
        {
            DisplayQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    // 퀴즈 종료 시 호출
    public void EndQuiz()
    {
        resultText.text = $"퀴즈 종료! 당신의 점수: {score}/{currentPartQuestions.Count}";
        // 여기에서 재시작 버튼 등을 활성화할 수 있음
    }
}
