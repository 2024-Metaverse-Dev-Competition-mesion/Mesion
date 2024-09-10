using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial;
    public ToDoListManager toDoListManager; // ToDoListManager 참조

    void Start()
    {
        // 모든 Renderer에 대한 원래 Material 저장
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterial;
        }
    }

    public void Highlight()
    {
        // 모든 하위 오브젝트의 Renderer에 highlightMaterial 적용
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = highlightMaterial;
        }

        Debug.Log($"{gameObject.name}가 강조되었습니다.");
    }

    public void RemoveHighlight()
    {
        // 모든 하위 오브젝트의 Renderer에 원래 Material 복원
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = originalMaterials[i];
        }

        Debug.Log($"{gameObject.name}의 강조가 해제되었습니다.");
    }

    // 클릭 시 현재 강조된 부품인지 확인
    private void OnMouseDown() // 마우스 클릭 이벤트
    {
        toDoListManager.OnPartClicked(this); // 현재 부품을 ToDoListManager로 전달
    }
}
