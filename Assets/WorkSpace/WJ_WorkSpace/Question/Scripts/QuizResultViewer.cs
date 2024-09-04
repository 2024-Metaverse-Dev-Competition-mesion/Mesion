using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizResultViewer : MonoBehaviour
{
    public QuizResultData quizResultData; // 퀴즈 결과 데이터
    public TextMeshProUGUI[] resultTexts; // 결과를 표시할 TextMeshPro 배열 (5개)
    public Button previousPageButton; // 이전 페이지 버튼
    public Button nextPageButton; // 다음 페이지 버튼
    public TextMeshProUGUI pageNumberText; // 페이지 번호를 표시할 TextMeshPro
    public TextMeshProUGUI detailsText; // 선택된 퀴즈의 세부 사항을 표시할 TextMeshPro

    public GameObject listPanel; // 제목 리스트 패널
    public GameObject detailsPanel; // 세부 퀴즈 내용을 표시할 패널

    public Button previousQuestionButton; // 이전 문제로 넘어가는 버튼
    public Button nextQuestionButton; // 다음 문제로 넘어가는 버튼

    private int currentPage = 1; // 현재 페이지
    private int itemsPerPage = 5; // 페이지 당 표시할 항목 수
    private int totalPage; // 총 페이지 수

    private QuizResult selectedResult; // 선택된 퀴즈 결과
    private int currentQuestionIndex = -1; // 현재 보여주고 있는 문제의 인덱스 (-1은 기본 정보 화면)

    void Start()
    {
        totalPage = Mathf.CeilToInt((float)quizResultData.quizResults.Count / itemsPerPage); // 전체 페이지 계산
        UpdatePage(); // 첫 페이지 업데이트
        previousPageButton.onClick.AddListener(GoToPreviousPage);
        nextPageButton.onClick.AddListener(GoToNextPage);

        // 각 제목에 클릭 리스너 추가
        for (int i = 0; i < resultTexts.Length; i++)
        {
            int index = i; // 로컬 변수로 인덱스를 저장하여 리스너에 전달
            resultTexts[i].GetComponent<Button>().onClick.AddListener(() => OnTitleClicked(index));
        }

        detailsPanel.SetActive(false); // 시작할 때는 세부 패널을 비활성화

        // 이전/다음 문제 버튼에 대한 리스너 추가
        previousQuestionButton.onClick.AddListener(GoToPreviousQuestion);
        nextQuestionButton.onClick.AddListener(GoToNextQuestion);
    }

    // 페이지를 업데이트하는 함수
    void UpdatePage()
    {
        int quizCount = quizResultData.quizResults.Count;

        // 현재 페이지에 맞는 항목 표시 (역순으로)
        int startItemIndex = (quizResultData.quizResults.Count - 1) - ((currentPage - 1) * itemsPerPage);

        for (int i = 0; i < itemsPerPage; i++)
        {
            int quizIndex = startItemIndex - i; // 역순으로 인덱스 계산

            if (quizIndex >= 0)
            {
                resultTexts[i].text = quizResultData.quizResults[quizIndex].quizTitle;
                resultTexts[i].gameObject.SetActive(true);
            }
            else
            {
                resultTexts[i].gameObject.SetActive(false); // 항목이 없으면 숨김
            }
        }

        // 페이지 번호 업데이트
        pageNumberText.text = $"{currentPage}/{totalPage}";

        // 버튼 보이기/숨기기 로직
        if (quizCount <= itemsPerPage)
        {
            // 문제 수가 5개 이하인 경우: 둘 다 숨기기
            previousPageButton.gameObject.SetActive(false);
            nextPageButton.gameObject.SetActive(false);
        }
        else
        {
            // 첫 페이지일 경우: Previous 버튼 숨기기
            previousPageButton.gameObject.SetActive(currentPage > 1);

            // 마지막 페이지일 경우: Next 버튼 숨기기
            nextPageButton.gameObject.SetActive(currentPage < totalPage);
        }
    }

    // 제목 클릭 시 호출되는 함수
    public void OnTitleClicked(int index)
    {
        int startItemIndex = (quizResultData.quizResults.Count - 1) - ((currentPage - 1) * itemsPerPage);
        int actualIndex = startItemIndex - index;

        // 제목 리스트 패널 비활성화, 세부 패널 활성화
        listPanel.SetActive(false);
        detailsPanel.SetActive(true);

        // 세부 퀴즈 내용 표시
        DisplaySelectedResult(actualIndex);
    }

    // 퀴즈 결과를 표시하는 함수 (처음 퀴즈 제목과 정보만 보여줌)
    public void DisplaySelectedResult(int index)
    {
        if (index < 0 || index >= quizResultData.quizResults.Count)
        {
            detailsText.text = "잘못된 선택입니다.";
            return;
        }

        selectedResult = quizResultData.quizResults[index];
        currentQuestionIndex = -1; // 처음에는 기본 정보만 출력
        DisplayBasicInfo(); // 기본 정보 출력

        // 이전/다음 문제 버튼 설정
        UpdateQuestionNavigationButtons();
    }

    // 기본 퀴즈 정보를 표시하는 함수
    private void DisplayBasicInfo()
    {
        StringBuilder resultDetails = new StringBuilder();
        resultDetails.AppendLine($"퀴즈 제목: {selectedResult.quizTitle}");
        resultDetails.AppendLine($"퀴즈 소요 시간: {selectedResult.totalTime}초");
        resultDetails.AppendLine($"최종 점수: {selectedResult.totalScore * 10}점");
        detailsText.text = resultDetails.ToString();
    }

    // 현재 문제를 표시하는 함수
    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex < 0 || currentQuestionIndex >= selectedResult.results.Count)
        {
            detailsText.text = "잘못된 선택입니다.";
            return;
        }

        QuestionResult questionResult = selectedResult.results[currentQuestionIndex];
        StringBuilder resultDetails = new StringBuilder();

        resultDetails.AppendLine($"{questionResult.questionText}\n\n");
        resultDetails.AppendLine($"  선택한 답: {questionResult.selectedAnswerText}");
        resultDetails.AppendLine($"  정답 여부: {(questionResult.isCorrect ? "O" : "X")}");

        if (!questionResult.isCorrect)
        {
            resultDetails.AppendLine($"  정답: {questionResult.correctAnswerText}");
        }

        resultDetails.AppendLine($"  파트: {GetPartName(questionResult.part)}");
        detailsText.text = resultDetails.ToString();

        // 이전/다음 문제 버튼 설정
        UpdateQuestionNavigationButtons();
    }

    // 이전/다음 버튼을 업데이트하는 함수
    private void UpdateQuestionNavigationButtons()
    {
        // 첫 번째 페이지일 때 (기본 정보 표시)
        previousQuestionButton.gameObject.SetActive(currentQuestionIndex >= 0); // 문제를 보는 중이면 이전 버튼 보이기
        nextQuestionButton.gameObject.SetActive(currentQuestionIndex < selectedResult.results.Count - 1); // 다음 문제가 있으면 활성화
    }

    // 이전 문제로 이동하는 함수
    public void GoToPreviousQuestion()
    {
        if (currentQuestionIndex > -1) // 기본 정보 화면에서 문제로 돌아갈 수 있음
        {
            currentQuestionIndex--;
            if (currentQuestionIndex == -1)
            {
                DisplayBasicInfo(); // 기본 정보로 돌아감
            }
            else
            {
                DisplayCurrentQuestion();
            }
        }
    }

    // 다음 문제로 이동하는 함수
    public void GoToNextQuestion()
    {
        if (currentQuestionIndex < selectedResult.results.Count - 1)
        {
            currentQuestionIndex++;
            DisplayCurrentQuestion();
        }
    }

    // 파트 이름을 반환하는 함수
    private string GetPartName(int part)
    {
        switch (part)
        {
            case 0:
                return "항공 법규";
            case 1:
                return "항공 기상";
            case 2:
                return "비행 이론 및 응용";
            default:
                return "알 수 없는 파트";
        }
    }

    // 이전 페이지로 이동
    void GoToPreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdatePage();
        }
    }

    // 다음 페이지로 이동
    void GoToNextPage()
    {
        if (currentPage < totalPage)
        {
            currentPage++;
            UpdatePage();
        }
    }
}