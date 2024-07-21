using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject itemPrefab; // 스폰할 아이템 프리팹
    public string itemName; // 아이템 이름

    public void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Instantiate(itemPrefab, position, rotation);
    }
}