using System.Collections;
using UnityEngine;

public class HighlightPart : MonoBehaviour
{
    private Material[] originalMaterials;
    public Material highlightMaterial; // ������ �� ����� ����
    public ToDoListManager toDoListManager; // ToDoListManager ����
    public float blinkInterval = 0.5f; // �����Ÿ� �ֱ� (�� ����)

    private bool isBlinking = false; // �����Ÿ� ���� ���� �÷���

    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterial; // sharedMaterial�� ���� ���� ����
        }
    }

    public void Highlight()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink()); // �����Ÿ� ����
        }
    }

    public void RemoveHighlight()
    {
        isBlinking = false; // �����Ÿ� ����
        StopCoroutine(Blink());

        // ��� ���� ������Ʈ�� Renderer�� ���� Material ����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = originalMaterials[i]; // sharedMaterial ����Ͽ� ���� ���� ����
        }

        Debug.Log($"{gameObject.name}�� ������ �����Ǿ����ϴ�.");
    }

    private IEnumerator Blink()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        while (isBlinking)
        {
            // ���� Material�� ����
            foreach (Renderer rend in renderers)
            {
                rend.sharedMaterial = highlightMaterial; // sharedMaterial ����Ͽ� ���� ����
            }

            yield return new WaitForSeconds(blinkInterval);

            // ���� Material�� ����
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sharedMaterial = originalMaterials[i]; // ���� ���� ����
            }

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // Ŭ�� �� ���� ������ ��ǰ���� Ȯ��
    private void OnMouseDown()
    {
        toDoListManager.OnPartClicked(this); // ���� ��ǰ�� ToDoListManager�� ����
    }
}
