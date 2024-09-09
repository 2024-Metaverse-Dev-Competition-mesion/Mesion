using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 관련 네임스페이스 추가

public class ToDoListManager : MonoBehaviour
{
    public List<HighlightPart> droneParts; // 드론 부품 리스트
    public List<GameObject> toDoItems; // ToDo 리스트 항목들 (TextMeshProUGUI가 포함된 UI 오브젝트)
    public QuizManager quizManager; // 퀴즈 매니저 연결
    public Button quizStartButton; // 퀴즈 시작 버튼
    public InteractionManager interactionManager; // InteractionManager 참조

    private int currentIndex = 0;
    private Color originalTextColor;

    void Start()
    {
        quizStartButton.gameObject.SetActive(false); // 시작 시 버튼 숨기기
        // 첫 번째 항목 강조 시작
        HighlightNextPart();
    }

    // 특정 부품이 클릭되었을 때 호출
    public void OnPartClicked(HighlightPart clickedPart)
    {
        // 클릭된 부품이 현재 강조된 부품인지 확인
        if (droneParts[currentIndex] == clickedPart)
        {
            CompleteCurrentItem();
        }
        else
        {
            Debug.Log("잘못된 부품이 클릭되었습니다. 현재 강조된 부품을 클릭하세요.");
        }
    }

    public void CompleteCurrentItem()
    {
        // 현재 부품의 강조 해제 및 ToDo 리스트 항목의 색상 복원
        if (currentIndex < droneParts.Count)
        {
            droneParts[currentIndex].RemoveHighlight();
            RestoreToDoItemColor(currentIndex);
            currentIndex++; // 다음 부품으로 이동
        }

        // 다음 부품이 존재할 경우 강조
        if (currentIndex < droneParts.Count)
        {
            HighlightNextPart();
        }
        else
        {
            Debug.Log("모든 부품이 완료되었습니다.");
            quizStartButton.gameObject.SetActive(true);
        }
    }
    public void OnQuizStartButtonClicked()
    {
        quizManager.StartQuiz(); // 퀴즈 시작
        quizStartButton.gameObject.SetActive(false); // 퀴즈 시작 후 버튼 숨기기
        interactionManager.enabled = false;
    }

    private void HighlightNextPart()
    {
        // 드론 부품 강조
        droneParts[currentIndex].Highlight();

        // ToDo 리스트 항목을 하얀색으로 강조
        ChangeToDoItemColor(currentIndex, Color.white);
    }

    private void ChangeToDoItemColor(int index, Color color)
    {
        // ToDo 리스트 항목의 TextMeshProUGUI 컴포넌트 가져오기
        TextMeshProUGUI itemText = toDoItems[index].GetComponent<TextMeshProUGUI>();

        if (itemText != null)
        {
            originalTextColor = itemText.color; // 원래 색상 저장
            itemText.color = color; // 하얀색으로 변경
        }
    }

    private void RestoreToDoItemColor(int index)
    {
        // ToDo 리스트 항목의 TextMeshProUGUI 컴포넌트를 가져와 원래 색상으로 복구
        TextMeshProUGUI itemText = toDoItems[index].GetComponent<TextMeshProUGUI>();

        if (itemText != null)
        {
            itemText.color = originalTextColor; // 원래 색상으로 복원
        }
    }
}
