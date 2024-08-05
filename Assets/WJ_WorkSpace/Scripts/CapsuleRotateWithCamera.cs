using UnityEngine;

public class CapsuleRotateWithCamera : MonoBehaviour
{
    public Transform cameraTransform; // 인스펙터에서 이 필드에 카메라를 드래그합니다.

    void Update()
    {
        // 캡슐의 회전을 카메라의 회전과 동기화합니다.
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}
