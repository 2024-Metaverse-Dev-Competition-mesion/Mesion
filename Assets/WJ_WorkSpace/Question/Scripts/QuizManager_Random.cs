using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager_Random : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI partInfoText;
    public Button[] optionsButtons;
    public QuizData[] quizDataArray;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<Question> selectedQuestions;

    void Start()
    {
        LoadQuestions();
        ShuffleQuestions();
        DisplayQuestion();
    }

    void LoadQuestions()
    {
        selectedQuestions = new List<Question>();
        System.Random rnd = new System.Random();

        for (int part = 0; part <= 2; part++)
        {
            int numberOfQuestionsToTake = 0;

            if (part == 0)
                numberOfQuestionsToTake = 3;
            else if (part == 1)
                numberOfQuestionsToTake = 3;
            else if (part == 2)
                numberOfQuestionsToTake = 4;

            foreach (var quizData in quizDataArray)
            {
                var partQuestions = quizData.questions.Where(q => q.part == part).ToList();

                if (partQuestions.Count <= numberOfQuestionsToTake)
                {
                    selectedQuestions.AddRange(partQuestions);
                }
                else
                {
                    selectedQuestions.AddRange(partQuestions.OrderBy(q => rnd.Next()).Take(numberOfQuestionsToTake));
                }
            }
        }

        if (selectedQuestions.Count == 0)
        {
            Debug.LogWarning("선택된 질문이 없습니다.");
            EndQuiz();
        }
    }

    void ShuffleQuestions()
    {
        System.Random rnd = new System.Random();
        selectedQuestions = selectedQuestions.OrderBy(q => rnd.Next()).ToList();
    }

    public void DisplayQuestion()
    {
        if (selectedQuestions == null || selectedQuestions.Count == 0)
        {
            resultText.text = "해당 파트에 질문이 없습니다.";
            return;
        }

        if (currentQuestionIndex >= selectedQuestions.Count)
        {
            EndQuiz();
            return;
        }

        // 이전 질문의 결과를 비활성화
        resultText.text = "";

        questionText.text = selectedQuestions[currentQuestionIndex].questionText;
        partInfoText.text = $"Part: {selectedQuestions[currentQuestionIndex].part}";

        for (int i = 0; i < optionsButtons.Length; i++)
        {
            if (i < selectedQuestions[currentQuestionIndex].options.Length)
            {
                optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = selectedQuestions[currentQuestionIndex].options[i];
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

        if (index == selectedQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
            resultText.text = "정답입니다!";
        }
        else
        {
            resultText.text = "오답입니다!";
        }

        Invoke(nameof(NextQuestion), 1.5f);
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < selectedQuestions.Count)
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
        resultText.text = $"퀴즈 종료! 당신의 점수: {score}/{selectedQuestions.Count}";
    }
}
