using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    public GameObject uiPanel; // 처음에 표시될 UI 패널

    void Start()
    {
        // UI가 이미 표시된 적이 있는지 확인
        if (PlayerPrefs.GetInt("HasShownUI", 0) == 0)
        {
            // 처음 시작 시 UI 패널 표시
            uiPanel.SetActive(true);
            PlayerPrefs.SetInt("HasShownUI", 1); // UI를 표시한 적이 있다고 저장
            PlayerPrefs.Save();  // PlayerPrefs를 디스크에 저장
        }
        else
        {
            // 이미 표시된 적이 있으면 UI 패널을 비활성화
            uiPanel.SetActive(false);
        }
    }

    // 프로그램이 종료될 때 호출되는 메서드
    void OnApplicationQuit()
    {
        // 프로그램 종료 시 UI 표시 상태를 초기화
        PlayerPrefs.DeleteKey("HasShownUI");
    }
}
