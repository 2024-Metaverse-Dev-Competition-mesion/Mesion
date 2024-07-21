using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject itemPrefab; // ������ ������ ������
    public string itemName; // ������ �̸�

    public void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Instantiate(itemPrefab, position, rotation);
    }
}