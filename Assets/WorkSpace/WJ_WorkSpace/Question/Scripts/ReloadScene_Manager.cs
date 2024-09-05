using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가

public class ReloadScene_Manager : MonoBehaviour
{
    // 씬을 다시 로드하는 메서드
    public void ReloadScene()
    {
        // 현재 활성화된 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}