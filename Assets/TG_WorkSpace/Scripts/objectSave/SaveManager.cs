using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public List<GameObject> objectsToSave = new List<GameObject>();

    public void AddObjectToSave(GameObject obj)
    {
        if (!objectsToSave.Contains(obj))
        {
            objectsToSave.Add(obj);
        }
    }

    public void Save()
    {
        SaveData saveData = new SaveData();

        foreach (var obj in objectsToSave)
        {
            ObjectData data = new ObjectData();
            data.name = obj.name;
            data.positionX = obj.transform.position.x;
            data.positionY = obj.transform.position.y;
            data.positionZ = obj.transform.position.z;
            data.rotationX = obj.transform.rotation.x;
            data.rotationY = obj.transform.rotation.y;
            data.rotationZ = obj.transform.rotation.z;
            data.rotationW = obj.transform.rotation.w;

            saveData.objects.Add(data);
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
        Debug.Log("Data Saved");
    }
}
