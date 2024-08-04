using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objects; // 빈 게임 오브젝트 배열
    public GameObject[] panels;
    private int currentIndex = 0; // 현재 활성화된 오브젝트의 인덱스

    void Start()
    {
        // 모든 오브젝트를 비활성화
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        // 첫 번째 오브젝트 활성화
        if (objects.Length > 0)
        {
            objects[0].SetActive(true);
        }
    }

    public void ToggleNext()
    {
        if (objects.Length == 0) return;

        // 현재 오브젝트 비활성화
        objects[currentIndex].SetActive(false);
        panels[currentIndex].SetActive(false);

        // 다음 오브젝트 활성화
        currentIndex = (currentIndex + 1) % objects.Length;
        objects[currentIndex].SetActive(true);
    }
}
