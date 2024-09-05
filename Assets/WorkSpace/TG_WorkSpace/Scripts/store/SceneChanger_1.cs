using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger_1 : MonoBehaviour
{
    public Button changeSceneButton;
    public string sceneName;  // ��ȯ�� ���� �̸�

    void Start()
    {
        changeSceneButton.onClick.AddListener(ChangeScene);  // ��ư Ŭ�� �� �� ��ȯ
    }

    void ChangeScene()
    {
        Debug.Log("Changing scene to " + sceneName);
        SceneManager.LoadScene(sceneName);  // �� ��ȯ
    }
}
