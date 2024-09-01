using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Inspector에서 설정할 패널
    public GameObject panel;
    public GameObject panel2;

    // 로드할 씬의 이름
    public string sceneName;

    private void Start()
    {
        // 시작 시 패널을 비활성화 상태로 설정
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    // 패널을 활성화하는 메서드
    public void ActivatePanel()
    {
        panel2.SetActive(false);
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    // 씬을 전환하는 메서드
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // 확인 버튼을 눌렀을 때 패널을 활성화하는 메서드
    public void ConfirmAndActivatePanel()
    {
        ActivatePanel();
    }
}
