using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        Debug.Log("Object Clicked: " + gameObject.name);
        // 오브젝트와 마우스 간의 거리 계산
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
        Debug.Log("OnMouseDown: Dragging started on " + gameObject.name);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
            Debug.Log("OnMouseDrag: Dragging " + gameObject.name + " to new position: " + transform.position);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Debug.Log("OnMouseUp: Dragging ended on " + gameObject.name);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}