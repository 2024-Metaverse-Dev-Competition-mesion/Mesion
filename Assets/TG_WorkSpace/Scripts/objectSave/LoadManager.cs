using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LoadManager : MonoBehaviour
{
    public GameObject objectPrefab;
    public SaveManager saveManager;

    public void Load()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            foreach (var data in saveData.objects)
            {
                GameObject obj = Instantiate(objectPrefab, new Vector3(data.positionX, data.positionY, data.positionZ),
                                             new Quaternion(data.rotationX, data.rotationY, data.rotationZ, data.rotationW));
                obj.name = data.name;
                saveManager.AddObjectToSave(obj); // 로드된 오브젝트를 SaveManager에 추가
            }

            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }
}
