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
    public TextMeshProUGUI timerText; 
    public Button[] optionsButtons; 
    public TMP_Dropdown questionDropdown; 
    public Button submitButton; // 제출 버튼 추가
    public GameObject warningPanel; // 경고 메시지를 표시할 패널 추가
    public TextMeshProUGUI warningText; // 경고 메시지를 표시할 텍스트 추가
    public Button closeWarningButton; // 경고 패널을 닫는 버튼 추가
    public GameObject confirmSubmitPanel; // 최종 제출 확인 패널 추가
    public Button confirmSubmitButton; // 최종 제출 확인 패널의 제출 버튼
    public Button cancelSubmitButton; // 최종 제출 확인 패널의 취소 버튼
    public GameObject resultPanel; // 결과를 보여주는 패널 추가
    public TextMeshProUGUI scoreText; // 결과 패널에 점수를 표시할 텍스트
    public QuizData[] quizDataArray;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<Question> selectedQuestions;
    private float timeRemaining = 600f; // 600 seconds (10 minutes)
    private int[] selectedAnswers; // 각 문제에 대해 선택된 답안을 저장
    private bool isQuizEnded = false; // 퀴즈 종료 상태를 나타내는 변수

    void Start()
    {
        LoadQuestions();
        ShuffleQuestions();
        selectedAnswers = new int[selectedQuestions.Count]; // 각 문제에 대해 선택된 답안을 저장할 배열 초기화
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            selectedAnswers[i] = -1; // 초기값은 -1로 설정하여 아무것도 선택되지 않은 상태를 나타냄
        }
        SetupDropdown(); 
        DisplayQuestion();
        StartCoroutine(TimerCountdown()); 

        // 제출 버튼 이벤트 리스너 추가
        submitButton.onClick.AddListener(SubmitQuiz);

        // 경고 패널 초기화 - 처음에는 비활성화
        warningPanel.SetActive(false);

        // 경고 패널 닫기 버튼에 대한 이벤트 리스너 추가
        closeWarningButton.onClick.AddListener(CloseWarningPanel);

        // 최종 제출 확인 패널 초기화 - 처음에는 비활성화
        confirmSubmitPanel.SetActive(false);

        // 최종 제출 확인 패널의 버튼에 대한 이벤트 리스너 추가
        confirmSubmitButton.onClick.AddListener(ConfirmFinalSubmit);
        cancelSubmitButton.onClick.AddListener(CloseConfirmSubmitPanel);

        // 결과 패널 초기화 - 처음에는 비활성화
        resultPanel.SetActive(false);
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

    // TMP_Dropdown 설정
    void SetupDropdown()
    {
        List<string> questionOptions = new List<string>();
        for (int i = 0; i < selectedQuestions.Count; i++)
        {
            questionOptions.Add($"문제 {i + 1}");
        }
        questionDropdown.ClearOptions();
        questionDropdown.AddOptions(questionOptions);
        questionDropdown.onValueChanged.AddListener(delegate { GoToQuestion(questionDropdown.value); });
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
            currentQuestionIndex = selectedQuestions.Count - 1; // 현재 문제를 마지막 문제로 유지
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
                optionsButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionsButtons[i].gameObject.SetActive(false);
            }
        }

        // 선택된 답안에 따라 버튼 색상 업데이트
        UpdateButtonColors();
        questionDropdown.SetValueWithoutNotify(currentQuestionIndex);
    }

    // 특정 문제로 이동
    void GoToQuestion(int index)
    {
        if (index >= 0 && index < selectedQuestions.Count)
        {
            currentQuestionIndex = index; // 선택된 문제로 이동
            DisplayQuestion(); // 문제를 화면에 표시
        }
        else
        {
            Debug.LogError("잘못된 질문 인덱스");
        }
    }

    // 사용자가 답안을 선택했을 때 호출되는 메서드
    public void OnAnswerSelected(int index)
    {
        if (index < 0 || index >= optionsButtons.Length)
        {
            Debug.LogError("잘못된 선택지 인덱스");
            return;
        }

        // 현재 질문의 선택된 답안을 배열에 저장
        selectedAnswers[currentQuestionIndex] = index;

        // 정답인지 체크
        if (index == selectedQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        // 선택한 답안에 따라 버튼 색상 업데이트
        UpdateButtonColors();

        // 마지막 문제가 아닐 경우에만 다음 문제로 넘어가기
        if (currentQuestionIndex < selectedQuestions.Count - 1)
        {
            currentQuestionIndex++;
            DisplayQuestion();
        }
    }

    // 선택된 답안에 따라 버튼 색상을 업데이트하는 메서드
    void UpdateButtonColors()
    {
        for (int i = 0; i < optionsButtons.Length; i++)
        {
            ColorBlock colors = optionsButtons[i].colors;
            if (i == selectedAnswers[currentQuestionIndex])
            {
                // 선택된 버튼은 다른 색상으로 설정
                colors.normalColor = Color.green;
            }
            else
            {
                // 선택되지 않은 버튼은 기본 색상으로 설정
                colors.normalColor = Color.white;
            }
            optionsButtons[i].colors = colors;
        }
    }

    // 퀴즈 제출 버튼을 눌렀을 때 호출되는 메서드
    public void SubmitQuiz()
    {
        List<int> unansweredQuestions = new List<int>();

        // 답을 선택하지 않은 문제를 체크
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            if (selectedAnswers[i] == -1)
            {
                unansweredQuestions.Add(i + 1); // 1부터 시작하는 문제 번호로 추가
            }
        }

        if (unansweredQuestions.Count > 0)
        {
            // 아직 답을 선택하지 않은 문제가 있을 때 경고 패널 활성화 및 메시지 표시
            string warningMessage = "다음 문제들이 아직 풀리지 않았습니다:\n";
            warningMessage += string.Join(", ", unansweredQuestions.Select(q => $"문제 {q}").ToArray());
            warningText.text = warningMessage; // 경고 패널의 텍스트에 메시지 설정
            warningPanel.SetActive(true); // 경고 패널 활성화
        }
        else
        {
            // 모든 문제가 풀렸다면 최종 제출 확인 패널 활성화
            confirmSubmitPanel.SetActive(true);
        }
    }

    // 경고 패널을 닫는 메서드
    public void CloseWarningPanel()
    {
        warningPanel.SetActive(false);
    }

    // 최종 제출 확인 패널의 취소 버튼을 눌렀을 때 호출되는 메서드
    public void CloseConfirmSubmitPanel()
    {
        confirmSubmitPanel.SetActive(false);
    }

    // 최종 제출 확인 패널의 제출 버튼을 눌렀을 때 호출되는 메서드
    public void ConfirmFinalSubmit()
    {
        confirmSubmitPanel.SetActive(false); // 확인 패널 비활성화
        EndQuiz(); // 퀴즈 종료 및 결과 표시
    }

    // End the quiz and display the score
    public void EndQuiz()
    {
        isQuizEnded = true; // 퀴즈가 종료됨을 표시
        StopCoroutine(TimerCountdown()); // 타이머 멈춤

        // 현재 할당된 모든 UI 요소 비활성화
        questionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        foreach (var button in optionsButtons)
        {
            button.gameObject.SetActive(false);
        }
        questionDropdown.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        warningPanel.SetActive(false);
        confirmSubmitPanel.SetActive(false);

        // 결과 패널 및 점수 텍스트만 활성화
        resultPanel.SetActive(true);

        // 점수에 따른 메시지 설정
        string resultMessage = $"당신의 점수는 {score * 10}점\n";
        if (score >= 7)
        {
            resultMessage += "합격입니다!";
        }
        else
        {
            resultMessage += "불합격입니다.";
        }

        // 결과 메시지 표시
        scoreText.text = resultMessage;
    }

    // Timer coroutine
    private IEnumerator TimerCountdown()
    {
        while (timeRemaining > 0)
        {
            if (isQuizEnded) yield break; // 퀴즈가 종료되었으면 타이머 종료
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
        warningPanel.SetActive(false); // 퀴즈가 종료되면 경고 패널 비활성화
    }
}