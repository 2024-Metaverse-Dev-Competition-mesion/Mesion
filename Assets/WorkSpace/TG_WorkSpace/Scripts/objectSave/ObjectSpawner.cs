using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;

    // Method to spawn an object
    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject newObj = Instantiate(objectPrefab, position, rotation);
        return newObj;
    }
}
