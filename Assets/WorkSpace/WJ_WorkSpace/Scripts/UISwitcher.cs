using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public GameObject[] panels; // 순서대로 A, B, C에 대한 참조 배열
    private int currentIndex = 0; // 현재 활성화된 창의 인덱스

    // 다음 패널로 전환하는 메서드
    public void NextPanel()
    {
        if (currentIndex < panels.Length - 1) // 다음 창이 있을 경우
        {
            panels[currentIndex].SetActive(false); // 현재 창 비활성화
            currentIndex++; // 인덱스 증가
            panels[currentIndex].SetActive(true); // 다음 창 활성화
        }
    }

    // 이전 패널로 돌아가는 메서드
    public void PreviousPanel()
    {
        if (currentIndex > 0) // 이전 창이 있을 경우
        {
            panels[currentIndex].SetActive(false); // 현재 창 비활성화
            currentIndex--; // 인덱스 감소
            panels[currentIndex].SetActive(true); // 이전 창 활성화
        }
    }
}
