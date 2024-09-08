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
        // DroneAssemblyManager 찾기
        assemblyManager = FindObjectOfType<DroneAssemblyManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null && draggedObject.tag == targetTag)
        {
            // 드롭된 오브젝트를 이 위치에 배치
            draggedObject.transform.SetParent(transform);
            draggedObject.transform.position = transform.position;

            Debug.Log(targetTag);
            // 부품이 올바르게 배치되었음을 DroneAssemblyManager에 알림
            assemblyManager.PlaceComponent(targetTag);
        }
    }
}