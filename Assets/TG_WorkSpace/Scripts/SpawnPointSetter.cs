using UnityEngine;

public class ObjectSpawnPositionSetter : MonoBehaviour
{
    [SerializeField]
    public Transform xrRigCamera; // XR Rig 카메라의 Transform을 참조

    [SerializeField]
    public float spawnDistance = 1.5f; // 카메라와 소환 지점 사이의 거리

    [SerializeField]
    public float heightOffset = 0.0f; // 높이 오프셋

    private ObjectSpawner objectSpawner; // ObjectSpawner 스크립트 참조

    void Start()
    {
        // ObjectSpawner 스크립트를 찾습니다.
        objectSpawner = GetComponent<ObjectSpawner>();

        if (objectSpawner == null)
        {
            Debug.LogError("ObjectSpawner component is not found on this GameObject.");
        }
    }

    void Update()
    {
        // 스페이스 키를 눌렀을 때 물체를 소환
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSpawnPositionAndSpawn();
        }
    }

    void SetSpawnPositionAndSpawn()
    {
        if (xrRigCamera == null || objectSpawner == null || objectSpawner.objectPrefab == null)
        {
            Debug.LogWarning("Necessary components or references are not set.");
            return;
        }

        // 소환 위치를 계산
        Vector3 spawnPosition = xrRigCamera.position + xrRigCamera.forward * spawnDistance;
        spawnPosition.y += heightOffset;

        // 소환 회전을 설정
        Quaternion spawnRotation = xrRigCamera.rotation;

        // ObjectSpawner의 SpawnObject 메서드를 호출하여 물체를 소환
        objectSpawner.SpawnObject(spawnPosition, spawnRotation);
    }
}
