using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager_Part : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonPartMapping
    {
        public Button button;
        public int partNumber;
    }

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI resultText;
    public Button[] optionsButtons;
    public QuizData[] quizDataArray;
    public ButtonPartMapping[] buttonPartMappings;  // 버튼과 파트 번호 매핑

    private int currentQuestionIndex = 0;
    private int score = 0;
    public int currentPart = 0;
    private List<Question> currentPartQuestions;

    void Start()
    {
        // 외부에서 할당된 버튼과 파트 매핑을 설정
        AssignButtonParts();

        // 초기 파트의 질문을 로드하고 표시
        LoadQuestionsForPart(currentPart);
        DisplayQuestion();
    }

    // 외부에서 지정한 버튼에 대한 파트를 할당
    void AssignButtonParts()
    {
        foreach (var mapping in buttonPartMappings)
        {
            if (mapping.button != null)
            {
                mapping.button.onClick.RemoveAllListeners();
                int partNumber = mapping.partNumber;
                mapping.button.onClick.AddListener(() => SetCurrentPart(partNumber));
            }
        }
    }

    // 선택된 파트에 따라 currentPart 변경
    void SetCurrentPart(int part)
    {
        currentPart = part;
        LoadQuestionsForPart(currentPart);
        DisplayQuestion();
    }

    void LoadQuestionsForPart(int part)
    {
        currentPartQuestions = new List<Question>();

        foreach (var quizData in quizDataArray)
        {
            var partQuestions = quizData.questions.Where(q => q.part == part).ToList();
            currentPartQuestions.AddRange(partQuestions);
        }

        if (currentPartQuestions.Count == 0)
        {
            Debug.LogWarning("선택된 파트에 질문이 없습니다.");
            EndQuiz();
        }
        else
        {
            System.Random rnd = new System.Random();
            currentPartQuestions = currentPartQuestions.OrderBy(q => rnd.Next()).Take(10).ToList();
        }
    }

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
                optionsButtons[i].onClick.RemoveAllListeners();
                int index = i;
                optionsButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                optionsButtons[i].gameObject.SetActive(false);
            }
        }
    }

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

    public void EndQuiz()
    {
        resultText.text = $"퀴즈 종료! 당신의 점수: {score}/{currentPartQuestions.Count}";
    }
}
