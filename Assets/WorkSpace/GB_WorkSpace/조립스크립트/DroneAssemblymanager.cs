using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssemblyManager : MonoBehaviour
{
    public GameObject completedDronePrefab;  // 완성된 드론 프리팹
    public Transform spawnPoint;  // 완성된 드론이 생성될 위치

    private bool isBadgePlaced = false;
    private bool[] isPropellerPlaced = new bool[6];

    // 각 드론 부품이 올바르게 배치되었는지 확인하는 함수
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

    // 모든 부품이 제자리에 놓였는지 확인
    private void CheckCompletion()
    {
        if (isBadgePlaced && isPropellerPlaced[0] && isPropellerPlaced[1] && isPropellerPlaced[2] && isPropellerPlaced[3] && isPropellerPlaced[4] && isPropellerPlaced[5])
        {
            // 모든 부품이 제자리에 배치되었을 때, 완성된 드론 프리팹을 생성
            CompleteDroneAssembly();
        }
    }

    // 완성된 드론 프리팹을 생성하는 함수
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

        // 완성된 드론 프리팹을 생성하고 지정된 위치에 배치
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
