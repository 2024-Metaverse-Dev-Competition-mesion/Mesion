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
                HighlightPart part = hit.collider.GetComponentInParent<HighlightPart>(); // 클릭된 오브젝트의 HighlightPart 컴포넌트 가져오기
                if (part != null)
                {
                    // 클릭된 부품을 ToDoListManager로 전달하여 현재 강조된 부품인지 확인
                    toDoListManager.OnPartClicked(part);
                }
            }
        }
    }
}
