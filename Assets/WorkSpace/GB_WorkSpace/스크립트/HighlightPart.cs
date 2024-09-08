using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial;

    void Start()
    {
        // 모든 Renderer에 대한 원래 Material 저장
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
        }
    }

    public void Highlight()
    {
        // 모든 하위 오브젝트의 Renderer에 highlightMaterial 적용
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.material = highlightMaterial;
        }

        Debug.Log($"{gameObject.name}가 강조되었습니다.");
    }

    public void RemoveHighlight()
    {
        // 모든 하위 오브젝트의 Renderer에 원래 Material 복원
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
        }

        Debug.Log($"{gameObject.name}의 강조가 해제되었습니다.");
    }
}