using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public string targetTag;
    private DroneAssemblyManager assemblyManager;

    private void Start()
    {
        // DroneAssemblyManager ã��
        assemblyManager = FindObjectOfType<DroneAssemblyManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null && draggedObject.tag == targetTag)
        {
            // ��ӵ� ������Ʈ�� �� ��ġ�� ��ġ
            draggedObject.transform.SetParent(transform);
            draggedObject.transform.position = transform.position;

            Debug.Log(targetTag);
            // ��ǰ�� �ùٸ��� ��ġ�Ǿ����� DroneAssemblyManager�� �˸�
            assemblyManager.PlaceComponent(targetTag);
        }
    }
}