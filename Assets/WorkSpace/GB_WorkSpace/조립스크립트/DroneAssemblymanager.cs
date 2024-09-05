using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssemblyManager : MonoBehaviour
{
    public GameObject completedDronePrefab;  // �ϼ��� ��� ������
    public Transform spawnPoint;  // �ϼ��� ����� ������ ��ġ

    private bool isBadgePlaced = false;
    private bool[] isPropellerPlaced = new bool[6];

    // �� ��� ��ǰ�� �ùٸ��� ��ġ�Ǿ����� Ȯ���ϴ� �Լ�
    public void PlaceComponent(string componentTag)
    {
        switch (componentTag)
        {
            case "badge":
                isBadgePlaced = true;
                Debug.Log("badge placed");
                break;
            case "pro1":
                isPropellerPlaced[0] = true;
                Debug.Log("1 placed");
                break;
            case "pro2":
                isPropellerPlaced[1] = true;
                Debug.Log("2 placed");
                break;
            case "pro3":
                isPropellerPlaced[2] = true;
                Debug.Log("3 placed");
                break;
            case "pro4":
                isPropellerPlaced[3] = true;
                Debug.Log("4 placed");
                break;
            case "pro5":
                isPropellerPlaced[4] = true;
                Debug.Log("5 placed");
                break;
            case "pro6":
                isPropellerPlaced[5] = true;
                Debug.Log("6 placed");
                break;
        }

        CheckCompletion();
    }

    // ��� ��ǰ�� ���ڸ��� �������� Ȯ��
    private void CheckCompletion()
    {
        if (isBadgePlaced && isPropellerPlaced[0] && isPropellerPlaced[1] && isPropellerPlaced[2] && isPropellerPlaced[3] && isPropellerPlaced[4] && isPropellerPlaced[5])
        {
            // ��� ��ǰ�� ���ڸ��� ��ġ�Ǿ��� ��, �ϼ��� ��� �������� ����
            CompleteDroneAssembly();
        }
    }

    // �ϼ��� ��� �������� �����ϴ� �Լ�
    private void CompleteDroneAssembly()
    {
        Debug.Log("Drone Assembly Complete!");


        DestroyWithTag("badge");
        DestroyWithTag("pro1");
        DestroyWithTag("pro2");
        DestroyWithTag("pro3");
        DestroyWithTag("pro4");
        DestroyWithTag("pro5");
        DestroyWithTag("pro6");
        DestroyWithTag("dronebody");

        // �ϼ��� ��� �������� �����ϰ� ������ ��ġ�� ��ġ
        GameObject drone = Instantiate(completedDronePrefab, spawnPoint.position, spawnPoint.rotation);

        Camera droneCamera = drone.GetComponentInChildren<Camera>();
        CameraSwitcher cameraSwitcher = FindObjectOfType<CameraSwitcher>();

        if (droneCamera != null && cameraSwitcher != null)
        {
            cameraSwitcher.droneCamera = droneCamera;
        }
    }

    private void DestroyWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
