using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    public Camera mainCamera;
    [SerializeField]
    public ToDoListManager toDoListManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ��
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ray�� �ε��� ������Ʈ�� ������ hit�� �����
                HighlightPart part = hit.collider.GetComponentInParent<HighlightPart>(); // Ŭ���� ������Ʈ�� HighlightPart ������Ʈ ��������
                if (part != null)
                {
                    // Ŭ���� ��ǰ�� ToDoListManager�� �����Ͽ� ���� ������ ��ǰ���� Ȯ��
                    toDoListManager.OnPartClicked(part);
                }
            }
        }
    }
}
