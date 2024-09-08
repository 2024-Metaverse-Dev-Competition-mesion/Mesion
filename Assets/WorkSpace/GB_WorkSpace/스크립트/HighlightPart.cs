using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial;

    void Start()
    {
        // ��� Renderer�� ���� ���� Material ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
        }
    }

    public void Highlight()
    {
        // ��� ���� ������Ʈ�� Renderer�� highlightMaterial ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.material = highlightMaterial;
        }

        Debug.Log($"{gameObject.name}�� �����Ǿ����ϴ�.");
    }

    public void RemoveHighlight()
    {
        // ��� ���� ������Ʈ�� Renderer�� ���� Material ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
        }

        Debug.Log($"{gameObject.name}�� ������ �����Ǿ����ϴ�.");
    }
}