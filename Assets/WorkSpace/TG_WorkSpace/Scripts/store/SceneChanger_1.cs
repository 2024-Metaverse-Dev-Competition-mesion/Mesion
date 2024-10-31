using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger_1 : MonoBehaviour
{
    public Button changeSceneButton;
    public string sceneName;  // 전환할 씬의 이름

    void Start()
    {
        changeSceneButton.onClick.AddListener(ChangeScene);  // 버튼 클릭 시 씬 전환
    }

    void ChangeScene()
    {
        Debug.Log("Changing scene to " + sceneName);
        SceneManager.LoadScene(sceneName);  // 씬 전환
    }
}
