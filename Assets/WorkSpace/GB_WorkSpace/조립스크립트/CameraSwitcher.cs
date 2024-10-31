using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;      // ���� ī�޶�
    public Camera droneCamera;     // ��� ī�޶�

    private bool isDroneCameraActive = false;  // ��� ī�޶� Ȱ��ȭ�Ǿ� �ִ��� ����

    void Start()
    {
        // ó������ ���� ī�޶� Ȱ��ȭ�ǰ� ��� ī�޶�� ��Ȱ��ȭ
        mainCamera.enabled = true;
        droneCamera.enabled = false;
    }

    public void SwitchCamera()
    {
        // ���� ī�޶� ���¸� ��ȯ
        if (isDroneCameraActive)
        {
            // ��� ī�޶󿡼� ���� ī�޶�� ��ȯ
            mainCamera.enabled = true;
            droneCamera.enabled = false;
        }
        else
        {
            // ���� ī�޶󿡼� ��� ī�޶�� ��ȯ
            mainCamera.enabled = false;
            droneCamera.enabled = true;
        }

        // ī�޶� ���� ���
        isDroneCameraActive = !isDroneCameraActive;
    }
}
