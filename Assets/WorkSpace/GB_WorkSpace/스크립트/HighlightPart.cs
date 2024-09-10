using System.Collections;
using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial; // 강조할 때 사용할 재질
    public ToDoListManager toDoListManager; // ToDoListManager 참조
    public float blinkInterval = 0.5f; // 깜빡거림 주기 (초 단위)

    private bool isBlinking = false; // 깜빡거림 상태 추적 플래그

    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterial; // sharedMaterial로 원래 재질 저장
        }
    }

    public void Highlight()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink()); // 깜빡거림 시작
        }
    }

    public void RemoveHighlight()
    {
        isBlinking = false; // 깜빡거림 종료
        StopCoroutine(Blink());

        // 모든 하위 오브젝트의 Renderer에 원래 Material 복원
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = originalMaterials[i]; // sharedMaterial 사용하여 원래 재질 복원
        }

        Debug.Log($"{gameObject.name}의 강조가 해제되었습니다.");
    }

    private IEnumerator Blink()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        while (isBlinking)
        {
            // 강조 Material로 변경
            foreach (Renderer rend in renderers)
            {
                rend.sharedMaterial = highlightMaterial; // sharedMaterial 사용하여 재질 변경
            }

            yield return new WaitForSeconds(blinkInterval);

            // 원래 Material로 복원
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sharedMaterial = originalMaterials[i]; // 원래 재질 복원
            }

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // 클릭 시 현재 강조된 부품인지 확인
    private void OnMouseDown()
    {
        toDoListManager.OnPartClicked(this); // 현재 부품을 ToDoListManager로 전달
    }
}
