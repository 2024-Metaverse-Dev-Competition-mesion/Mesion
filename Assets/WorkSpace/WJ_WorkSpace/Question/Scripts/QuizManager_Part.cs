using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager_Part : MonoBehaviour
{
    // 파트 이름을 저장하는 배열 추가
    private string[] partNames = { "항공 법규", "항공 기상", "비행 이론 및 응용" };

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
    public ButtonPartMapping[] buttonPartMappings;
    public GameObject ResultPanel;

    public QuizResultData quizResultData;  // 퀴즈 결과를 저장할 ScriptableObject 추가

    private int currentQuestionIndex = 0;
    private int score = 0;
    public int currentPart = 0; // 현재 파트를 나타내는 변수
    private List<Question> currentPartQuestions;

    private DateTime quizStartTime; // 퀴즈 시작 시간을 기록

    void Start()
    {
        quizStartTime = DateTime.Now; // 퀴즈 시작 시간 기록

        AssignButtonParts();
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

    // 선택된 파트의 질문을 로드하는 메서드
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

    // 질문을 화면에 표시하는 메서드
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

    // 선택한 답을 처리하는 메서드
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

    // 퀴즈 종료 시 호출되는 메서드
    public void EndQuiz()
    {
        ResultPanel.SetActive(true);
        resultText.text = $"퀴즈 종료! 당신의 점수: {score}/{currentPartQuestions.Count}";

        questionText.gameObject.SetActive(false);

        foreach (var button in optionsButtons)
        {
            button.gameObject.SetActive(false);
        }

        // 퀴즈 결과 저장
        SaveQuizResult();
    }

    // 퀴즈 결과를 저장하는 메서드
    private void SaveQuizResult()
    {
        float quizDuration = (float)(DateTime.Now - quizStartTime).TotalSeconds;

        // 현재 날짜를 "MM/dd" 형식으로 저장
        string currentDate = DateTime.Now.ToString("MM/dd");

        // score를 10배로 곱해서 점수 표시
        int finalScore = score * 10;

        // currentPart에 해당하는 파트 이름 가져오기
        string partName = currentPart >= 0 && currentPart < partNames.Length ? partNames[currentPart] : "알 수 없는 파트";

        QuizResult currentQuizResult = new QuizResult
        {
            // 실전 모드 (파트 이름) [점수] 형식으로 제목 생성
            quizTitle = $"연습 모드 ({partName}) {currentDate} [{finalScore}점]",
            totalTime = quizDuration,
            totalScore = score,
            results = new List<QuestionResult>()
        };

        for (int i = 0; i < currentPartQuestions.Count; i++)
        {
            Question question = currentPartQuestions[i];

            int selectedAnswerIndex = Mathf.Clamp(i, 0, question.options.Length - 1);

            bool isCorrect = selectedAnswerIndex == question.correctAnswerIndex;
            string correctAnswerText = question.options[question.correctAnswerIndex];

            QuestionResult questionResult = new QuestionResult
            {
                questionText = question.questionText,
                selectedAnswerText = question.options[selectedAnswerIndex],
                selectedAnswerIndex = selectedAnswerIndex,
                isCorrect = isCorrect,
                correctAnswerText = isCorrect ? "" : correctAnswerText,
                part = question.part
            };

            currentQuizResult.results.Add(questionResult);
        }

        quizResultData.quizResults.Add(currentQuizResult);

        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(quizResultData);
        #endif
    }
}