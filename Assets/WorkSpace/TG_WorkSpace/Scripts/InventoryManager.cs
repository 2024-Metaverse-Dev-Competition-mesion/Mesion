using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inventoryItems;
    public Transform spawnPoint; // XR Rig�� ī�޶� �� ��ġ
    public SaveManager saveManager; // SaveManager �߰�

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
    }
}
