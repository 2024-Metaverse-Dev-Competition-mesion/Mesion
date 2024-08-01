using UnityEngine;

public class SpawnedObjectsManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;

    void Start()
    {
        // 예시: ObjectSpawner의 멤버 사용
        objectSpawner.spawnAsChildren = true;
        objectSpawner.RandomizeSpawnOption();
        Debug.Log("Spawn Option Index: " + objectSpawner.spawnOptionIndex);

        // 예시: 오브젝트 생성
        Vector3 spawnPosition = new Vector3(0, 0, 0); // 예시 위치
        Quaternion spawnRotation = Quaternion.identity; // 예시 회전
        objectSpawner.SpawnObject(spawnPosition, spawnRotation);
    }
}
