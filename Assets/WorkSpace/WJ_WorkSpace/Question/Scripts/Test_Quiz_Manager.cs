using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Test_Quiz_Manager : MonoBehaviour
{
    // UI 요소들
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timerText; 
    public Button[] optionsButtons; 
    public TMP_Dropdown questionDropdown; 
    public Button submitButton;
    public GameObject warningPanel;
    public TextMeshProUGUI warningText;
    public Button closeWarningButton;
    public GameObject confirmSubmitPanel;
    public Button confirmSubmitButton;
    public Button cancelSubmitButton;
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;

    // 퀴즈 데이터 및 결과
    public QuizData[] quizDataArray;
    public QuizResultData quizResultData; // 퀴즈 결과를 저장할 ScriptableObject 추가
    private List<Question> selectedQuestions; // 선택된 질문 리스트
    private int[] selectedAnswers; // 선택된 답안의 인덱스
    private int score; // 점수
    private int currentQuestionIndex = 0; // 현재 문제 인덱스
    private bool isQuizEnded = false; // 퀴즈 종료 여부
    private DateTime quizStartTime; // 퀴즈 시작 시간 기록

    void Start()
    {
        InitializeQuiz();
        SetupUIInteraction();
    }

    void OnEnable()
    {
        // 다시 활성화될 때 퀴즈 초기화
        InitializeQuiz();

        // 비활성화된 UI 요소들 다시 활성화
        if (questionText != null)
            questionText.gameObject.SetActive(true);
        
        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (questionDropdown != null)
            questionDropdown.gameObject.SetActive(true);

        if (submitButton != null)
            submitButton.gameObject.SetActive(true);
    }

    // 게임 오브젝트가 비활성화될 때 호출되는 메서드
    void OnDisable()
    {
        ResetQuiz();
    }

    // 퀴즈 초기화 메서드
    void InitializeQuiz()
    {
        quizStartTime = DateTime.Now; // 퀴즈 시작 시간 기록
        LoadQuestions();
        ShuffleQuestions();
        selectedAnswers = new int[selectedQuestions.Count];
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            selectedAnswers[i] = -1; // 답을 선택하지 않은 상태는 -1로 설정
        }
        SetupDropdown();
        DisplayQuestion();
        StartCoroutine(TimerCountdown());

        // 제출 버튼 이벤트 리스너 추가
        submitButton.onClick.AddListener(SubmitQuiz);

        // 경고 패널 초기화
        warningPanel.SetActive(false);

        // 경고 패널 닫기 버튼 이벤트 리스너 추가
        closeWarningButton.onClick.AddListener(CloseWarningPanel);

        // 최종 제출 확인 패널 초기화 및 리스너 추가
        confirmSubmitPanel.SetActive(false);
        confirmSubmitButton.onClick.AddListener(ConfirmFinalSubmit);
        cancelSubmitButton.onClick.AddListener(CloseConfirmSubmitPanel);

        // 결과 패널 초기화
        resultPanel.SetActive(false);

        // 점수 및 상태 초기화
        score = 0;
        currentQuestionIndex = 0;
        isQuizEnded = false;
    }

    // 퀴즈를 초기 상태로 되돌리는 메서드
    void ResetQuiz()
    {
        StopAllCoroutines(); // 타이머 코루틴 정지

        // 선택된 답안 배열 초기화
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            selectedAnswers[i] = -1; // 초기화
        }

        // 점수 및 상태 초기화
        score = 0;
        currentQuestionIndex = 0;
        isQuizEnded = false;

        // UI 요소 초기화
        questionText.text = "";
        timerText.text = "00:00";
        foreach (var button in optionsButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.gameObject.SetActive(false);
        }
        questionDropdown.ClearOptions();
        warningPanel.SetActive(false);
        confirmSubmitPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    // 질문을 로드하는 메서드
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

    // 질문 리스트를 섞는 메서드
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

    // 특정 문제로 이동
    void GoToQuestion(int index)
    {
        if (index >= 0 && index < selectedQuestions.Count)
        {
            currentQuestionIndex = index;
            DisplayQuestion();
        }
        else
        {
            Debug.LogError("잘못된 질문 인덱스");
        }
    }

    // 질문을 표시하는 메서드
    public void DisplayQuestion()
    {
        if (selectedQuestions == null || selectedQuestions.Count == 0)
        {
            resultText.text = "해당 파트에 질문이 없습니다.";
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
                optionsButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionsButtons[i].gameObject.SetActive(false);
            }
        }

        UpdateButtonColors();
        questionDropdown.SetValueWithoutNotify(currentQuestionIndex);
    }

    // 답안 선택
    public void OnAnswerSelected(int index)
    {
        selectedAnswers[currentQuestionIndex] = index;

        // 정답인지 체크
        if (index == selectedQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        UpdateButtonColors();

        if (currentQuestionIndex < selectedQuestions.Count - 1)
        {
            currentQuestionIndex++;
            DisplayQuestion();
        }
    }

    // 선택한 답안에 따라 버튼 색상 업데이트
    void UpdateButtonColors()
    {
        for (int i = 0; i < optionsButtons.Length; i++)
        {
            ColorBlock colors = optionsButtons[i].colors;
            if (i == selectedAnswers[currentQuestionIndex])
            {
                colors.normalColor = Color.green;
            }
            else
            {
                colors.normalColor = Color.white;
            }
            optionsButtons[i].colors = colors;
        }
    }

    public void SubmitQuiz()
    {
        List<int> unansweredQuestions = new List<int>();

        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            if (selectedAnswers[i] == -1)
            {
                unansweredQuestions.Add(i + 1);
            }
        }

        if (unansweredQuestions.Count > 0)
        {
            string warningMessage = "다음 문제들이 아직 풀리지 않았습니다:\n";
            warningMessage += string.Join(", ", unansweredQuestions.Select(q => $"문제 {q}").ToArray());
            warningText.text = warningMessage;
            warningPanel.SetActive(true);
            SetUIInteractable(false); // UI 상호작용 불가능
        }
        else
        {
            confirmSubmitPanel.SetActive(true);
            SetUIInteractable(false); // UI 상호작용 불가능
        }
    }

    // 최종 제출 확인 패널에서 제출 버튼 클릭
    public void ConfirmFinalSubmit()
    {
        confirmSubmitPanel.SetActive(false);
        EndQuiz();
    }

    // 퀴즈 종료 및 결과 저장
    public void EndQuiz()
    {
        if (isQuizEnded) // 이미 퀴즈가 끝났다면 return으로 종료
            return;

        isQuizEnded = true; // 퀴즈 종료 처리
        StopCoroutine(TimerCountdown());

        SaveQuizResult(); // 퀴즈 결과 저장

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

        resultPanel.SetActive(true);

        string resultMessage = $"당신의 점수는 {score * 10}점\n";
        if (score >= 7)
        {
            resultMessage += "합격입니다!";
        }
        else
        {
            resultMessage += "불합격입니다.";
        }

        scoreText.text = resultMessage;
    }

    // 퀴즈 결과 저장
    private void SaveQuizResult()
    {
        float quizDuration = (float)(DateTime.Now - quizStartTime).TotalSeconds;

        // 현재 날짜를 "MM/dd" 형식으로 저장
        string currentDate = DateTime.Now.ToString("MM/dd");

        // score를 10배로 곱해서 점수 표시
        int finalScore = score * 10;

        QuizResult currentQuizResult = new QuizResult
        {
            // 실전 모드, 날짜, 점수를 포함한 제목 생성
            quizTitle = $"실전 모드 {currentDate} [{finalScore}점]",
            totalTime = quizDuration,
            totalScore = score,
            results = new List<QuestionResult>()
        };

        for (int i = 0; i < selectedQuestions.Count; i++)
        {
            Question question = selectedQuestions[i];
            bool isCorrect = selectedAnswers[i] == question.correctAnswerIndex;
            string correctAnswerText = question.options[question.correctAnswerIndex];

            QuestionResult questionResult = new QuestionResult
            {
                questionText = question.questionText,
                selectedAnswerText = question.options[selectedAnswers[i]],
                selectedAnswerIndex = selectedAnswers[i],
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

    void SetupUIInteraction()
    {
        // 경고 패널이 활성화되거나 비활성화될 때 상호작용 처리
        warningPanel.SetActive(false);
        confirmSubmitPanel.SetActive(false);
        warningPanel.SetActive(false);

        // 비활성화 시 UI 요소들을 클릭 불가 상태로 설정
        warningPanel.SetActive(false);
        confirmSubmitPanel.SetActive(false);
        
        closeWarningButton.onClick.AddListener(CloseWarningPanel);
        cancelSubmitButton.onClick.AddListener(CloseConfirmSubmitPanel);
    }

    // 상호작용 가능한 상태로 전환하는 메서드
    void SetUIInteractable(bool state)
    {
        foreach (var button in optionsButtons)
        {
            button.interactable = state;
        }
        questionDropdown.interactable = state;
        submitButton.interactable = state;
    }

    // 경고 패널을 닫고 상호작용 상태 복구
    public void CloseWarningPanel()
    {
        warningPanel.SetActive(false);
        SetUIInteractable(true);
    }

    // 최종 제출 확인 패널을 닫고 상호작용 상태 복구
    public void CloseConfirmSubmitPanel()
    {
        confirmSubmitPanel.SetActive(false);
        SetUIInteractable(true);
    }

    // 타이머 시작
    private IEnumerator TimerCountdown()
    {
        float timeRemaining = 600f; // 10분 타이머
        while (timeRemaining > 0 && !isQuizEnded)
        {
            timeRemaining -= 1;
            UpdateTimerText(timeRemaining);
            yield return new WaitForSeconds(1f);
        }
        if (timeRemaining <= 0)
        {
            TimeUp();
        }
    }

    // 타이머 텍스트 업데이트
    private void UpdateTimerText(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 시간 초과 처리
    private void TimeUp()
    {
        resultText.text = $"시간 초과! 당신의 점수: {score}/{selectedQuestions.Count}";
        foreach (var button in optionsButtons)
        {
            button.interactable = false;
        }
        warningPanel.SetActive(false);
    }
}
