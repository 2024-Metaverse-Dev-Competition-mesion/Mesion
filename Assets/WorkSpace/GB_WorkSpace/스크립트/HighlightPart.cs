using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial;
    public ToDoListManager toDoListManager; // ToDoListManager ����

    void Start()
    {
        // ��� Renderer�� ���� ���� Material ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterial;
        }
    }

    public void Highlight()
    {
        // ��� ���� ������Ʈ�� Renderer�� highlightMaterial ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = highlightMaterial;
        }

        Debug.Log($"{gameObject.name}�� �����Ǿ����ϴ�.");
    }

    public void RemoveHighlight()
    {
        // ��� ���� ������Ʈ�� Renderer�� ���� Material ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = originalMaterials[i];
        }

        Debug.Log($"{gameObject.name}�� ������ �����Ǿ����ϴ�.");
    }

    // Ŭ�� �� ���� ������ ��ǰ���� Ȯ��
    private void OnMouseDown() // ���콺 Ŭ�� �̺�Ʈ
    {
        toDoListManager.OnPartClicked(this); // ���� ��ǰ�� ToDoListManager�� ����
    }
}
