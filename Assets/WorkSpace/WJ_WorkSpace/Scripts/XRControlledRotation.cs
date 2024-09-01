using UnityEngine;

public class XRControlledRotation : MonoBehaviour
{
    public Transform xrCameraTransform; // XR 카메라 Transform에 대한 참조
    public Transform targetObject; // 빈 게임 오브젝트의 Transform (VRController)

    void Update()
    {
        // 카메라의 회전을 빈 게임 오브젝트에 적용
        UpdateObjectRotation();
    }

    private void UpdateObjectRotation()
    {
        if (xrCameraTransform != null && targetObject != null)
        {
            // 카메라의 회전을 빈 게임 오브젝트에 적용
            targetObject.rotation = Quaternion.Euler(0, xrCameraTransform.eulerAngles.y, 0);
        }
    }
}
