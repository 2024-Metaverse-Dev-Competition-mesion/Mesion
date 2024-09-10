using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    public GameObject uiPanel; // 처음에 표시될 UI 패널
    private bool isUIPanelShown = false; // flag 변수로 UI 패널 표시 여부 관리
    public GameObject tmp; // 처음에 표시될 UI 패널

    void Start()
    {
        DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않도록 설정

        // UI가 표시된 적이 없으면 표시
        if (!isUIPanelShown)
        {
            uiPanel.SetActive(true); // UI 패널을 활성화
            isUIPanelShown = true;   // 표시했다고 flag를 설정
        }
        else
        {
            uiPanel.SetActive(false); // 이미 표시되었으면 UI 패널 비활성화
        }
    }
}
