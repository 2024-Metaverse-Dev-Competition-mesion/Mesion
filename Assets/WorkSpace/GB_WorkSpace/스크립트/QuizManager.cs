using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 관련 네임스페이스 추가

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel; // 4지선다 버튼들이 포함된 패널
    public List<HighlightPart> droneParts; // 드론 부품 리스트
    public TextMeshProUGUI questionText; // 문제 텍스트
    public Button[] answerButtons; // 4지선다 버튼들 (TextMeshProUGUI 사용)
    private HighlightPart correctPart; // 정답 부품
    public TextMeshProUGUI resultText; // 정답/오답 메시지 텍스트

    private List<int> randomOrder = new List<int>(); // 무작위 부품 순서
    private int quizIndex = 0; // 현재 퀴즈의 인덱스

    void Start()
    {
        quizPanel.SetActive(false); // 처음에 패널을 숨기기
        resultText.gameObject.SetActive(false); // 처음에 결과 텍스트 숨기기
    }

    // 퀴즈 시작 버튼을 누르면 호출되는 함수
    public void StartQuiz()
    {
        quizPanel.SetActive(true); // 패널을 활성화하여 퀴즈 시작
        InitializeQuiz();
        DisplayNextQuestion();
    }

    void InitializeQuiz()
    {
        // 부품을 무작위로 섞기
        for (int i = 0; i < droneParts.Count; i++)
        {
            randomOrder.Add(i);
        }

        // 무작위 순서로 정렬
        randomOrder.Shuffle(); // List를 섞는 확장 메서드를 사용할 수 있음
    }

    public void DisplayNextQuestion()
    {
        if (quizIndex >= randomOrder.Count)
        {
            Debug.Log("퀴즈 완료!");
            quizPanel.SetActive(false); // 퀴즈 완료 시 패널을 숨김
            return;
        }

        // 정답 부품 선택 및 강조
        correctPart = droneParts[randomOrder[quizIndex]];
        correctPart.Highlight();

        // 퀴즈 문제 텍스트 생성
        questionText.text = "이 부품의 이름은 무엇일까요?";

        // 4지선다 문제 생성
        GenerateMultipleChoiceQuestion();

        quizIndex++;
    }

    void GenerateMultipleChoiceQuestion()
    {
        // 정답 버튼을 랜덤 위치에 배치
        int correctAnswerIndex = Random.Range(0, answerButtons.Length);
        answerButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text = correctPart.gameObject.name;

        // 오답 생성
        List<int> remainingParts = new List<int>(randomOrder);
        remainingParts.Remove(randomOrder[quizIndex]); // 정답 제외

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i == correctAnswerIndex) continue; // 정답은 이미 설정됨

            int randomWrongIndex = Random.Range(0, remainingParts.Count);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = droneParts[remainingParts[randomWrongIndex]].gameObject.name;
            remainingParts.RemoveAt(randomWrongIndex); // 중복 방지를 위해 제거
        }
    }

    // 사용자가 답을 선택했을 때 호출
    public void OnAnswerSelected(int selectedIndex)
    {
        // 정답 확인
        if (answerButtons[selectedIndex].GetComponentInChildren<TextMeshProUGUI>().text == correctPart.gameObject.name)
        {
            Debug.Log("정답!");
            resultText.text = "정답!";
            // 정답 처리 후 다음 문제로 넘어가기
            correctPart.RemoveHighlight();
            DisplayNextQuestion();
        }
        else
        {
            Debug.Log("오답! 정답은 " + correctPart.gameObject.name);
            resultText.text = "오답! 정답은 " + correctPart.gameObject.name;
            correctPart.RemoveHighlight();
            DisplayNextQuestion(); // 오답 시에도 다음 문제로 넘어가기
        }
    }
}
public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
