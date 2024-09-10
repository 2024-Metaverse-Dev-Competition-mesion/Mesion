using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;          // 아이템 이름
    public string description;       // 아이템 설명
    public int itemID;               // 아이템 ID
    public int price;                // 아이템 가격

    // 기기 종류를 위한 변수 (enum 또는 string 선택 가능)
    public string deviceType;        // 기기 종류 (예: 드론, 카메라 등)

    // 기기 이미지를 위한 변수 (Sprite 타입)
    public Sprite deviceImage;       // 기기 이미지

    // 특정 오브젝트(프리팹)를 불러오기 위한 변수
    public GameObject itemPrefab;

    // 아이템을 구매하는 메서드
    public void Purchase()
    {
        Debug.Log(itemName + " has been purchased.");
    }

    // 특정 위치에 아이템 오브젝트를 생성하는 메서드
    public void SpawnItem(Vector3 position, Quaternion rotation)
    {
        if (itemPrefab != null)
        {
            // itemPrefab을 주어진 위치와 회전으로 생성
            GameObject spawnedItem = Instantiate(itemPrefab, position, rotation);
            Debug.Log(itemName + " has been spawned at position: " + position);
        }
        else
        {
            Debug.LogError("No prefab assigned to " + itemName);
        }
    }
}
