using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;      // 메인 카메라
    public Camera droneCamera;     // 드론 카메라

    private bool isDroneCameraActive = false;  // 드론 카메라가 활성화되어 있는지 여부

    void Start()
    {
        // 처음에는 메인 카메라가 활성화되고 드론 카메라는 비활성화
        mainCamera.enabled = true;
        droneCamera.enabled = false;
    }

    public void SwitchCamera()
    {
        // 현재 카메라 상태를 전환
        if (isDroneCameraActive)
        {
            // 드론 카메라에서 메인 카메라로 전환
            mainCamera.enabled = true;
            droneCamera.enabled = false;
        }
        else
        {
            // 메인 카메라에서 드론 카메라로 전환
            mainCamera.enabled = false;
            droneCamera.enabled = true;
        }

        // 카메라 상태 토글
        isDroneCameraActive = !isDroneCameraActive;
    }
}
