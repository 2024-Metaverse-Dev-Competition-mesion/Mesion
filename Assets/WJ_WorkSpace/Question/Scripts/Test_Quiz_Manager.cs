using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test_Quiz_Manager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timerText; // Ensure this is assigned in the Inspector
    public Button[] optionsButtons;
    public QuizData[] quizDataArray;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<Question> selectedQuestions;
    private float timeRemaining = 600f; // 600 seconds (10 minutes)

    void Start()
    {
        LoadQuestions();
        ShuffleQuestions();
        DisplayQuestion();
        StartCoroutine(TimerCountdown()); // Start the timer coroutine
    }

    // Load the required number of questions from each part
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

    // Shuffle the selected questions
    void ShuffleQuestions()
    {
        System.Random rnd = new System.Random();
        selectedQuestions = selectedQuestions.OrderBy(q => rnd.Next()).ToList();
    }

    // Display the current question
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

        questionText.text = selectedQuestions[currentQuestionIndex].questionText;

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

    // Handle the selection of an answer
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
        }

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

    // End the quiz and display the score
    public void EndQuiz()
    {
        resultText.text = $"퀴즈 종료! 당신의 점수: {score}/{selectedQuestions.Count}";
    }

    // Timer coroutine
    private IEnumerator TimerCountdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= 1;
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
        }
        TimeUp();
    }

    // Update the timer text
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Handle time up
    private void TimeUp()
    {
        resultText.text = $"시간 초과! 당신의 점수: {score}/{selectedQuestions.Count}";
        foreach (var button in optionsButtons)
        {
            button.interactable = false;
        }
    }
}