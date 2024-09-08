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
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ray가 부딪힌 오브젝트의 정보가 hit에 저장됨
                HighlightPart part = hit.collider.GetComponent<HighlightPart>();
                Debug.Log(part);
                if (part != null)
                {
                    // 부품을 만진 것으로 간주
                    Debug.Log("hit");
                    toDoListManager.CompleteCurrentItem(); // ToDo 리스트의 현재 부품 상호작용 완료
                }
            }
        }
    }
}