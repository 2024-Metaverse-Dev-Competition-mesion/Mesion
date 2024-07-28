using UnityEngine;

public class SpawnedObjectsManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;

    void Start()
    {
        // Example of setting spawnAsChildren
        objectSpawner.spawnAsChildren = true;

        // Example of calling RandomizeSpawnOption
        objectSpawner.RandomizeSpawnOption();

        // Example of using spawnOptionIndex
        int optionIndex = objectSpawner.spawnOptionIndex;
        Debug.Log("Spawn Option Index: " + optionIndex);

        // Example of spawning an object
        objectSpawner.SpawnObject(new Vector3(0, 0, 0), Quaternion.identity);
    }
}
