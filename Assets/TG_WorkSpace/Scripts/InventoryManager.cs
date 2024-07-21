using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inventoryItems;
    public Transform spawnPoint; // XR Rig의 카메라 앞 위치

    void Start()
    {
        foreach (var item in inventoryItems)
        {
            Button itemButton = item.GetComponent<Button>();
            itemButton.onClick.AddListener(() => OnItemClicked(item));
        }
    }

    public void OnItemClicked(InventoryItem item)
    {
        item.SpawnItem(spawnPoint.position, spawnPoint.rotation);
    }
}
