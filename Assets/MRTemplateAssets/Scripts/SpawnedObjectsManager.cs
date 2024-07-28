using UnityEngine;

public class SpawnedObjectsManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;

    void Start()
    {
        // ����: ObjectSpawner�� ��� ���
        objectSpawner.spawnAsChildren = true;
        objectSpawner.RandomizeSpawnOption();
        Debug.Log("Spawn Option Index: " + objectSpawner.spawnOptionIndex);

        // ����: ������Ʈ ����
        Vector3 spawnPosition = new Vector3(0, 0, 0); // ���� ��ġ
        Quaternion spawnRotation = Quaternion.identity; // ���� ȸ��
        objectSpawner.SpawnObject(spawnPosition, spawnRotation);
    }
}
