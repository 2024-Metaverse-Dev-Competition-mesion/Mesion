using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 startPosition;
    private Transform originalParent;
    private DroneAssemblyManager assemblyManager;

    void Start()
    {
        startPosition = transform.position;
        originalParent = transform.parent;

        assemblyManager = FindObjectOfType<DroneAssemblyManager>();

        if (assemblyManager == null)
        {
            Debug.LogError("DroneAssemblyManager가 씬에 없습니다.");
        }
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
        Debug.Log("OnMouseDown: Dragging started on " + gameObject.name);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Debug.Log("OnMouseUp: Dragging ended on " + gameObject.name);

        // DropZone과 충돌 여부를 확인
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            DropHandler dropHandler = hit.collider.GetComponent<DropHandler>();
            if (dropHandler != null && dropHandler.targetTag == gameObject.tag)
            {
                // 올바른 DropZone에 드롭된 경우
                transform.position = hit.transform.position;
                transform.SetParent(hit.transform);
                Debug.Log("Dropped at " + hit.collider.name);
                assemblyManager.PlaceComponent(gameObject.tag);
            }
            else
            {
                // 올바르지 않은 DropZone에 드롭된 경우
                transform.position = startPosition;
                transform.SetParent(originalParent);
                Debug.Log("Dropped at invalid position, returning to start position.");
            }
        }
        else
        {
            // 아무것도 충돌하지 않은 경우 원래 위치로 돌아가기
            transform.position = startPosition;
            transform.SetParent(originalParent);
            Debug.Log("Dropped outside any valid zone, returning to start position.");
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}