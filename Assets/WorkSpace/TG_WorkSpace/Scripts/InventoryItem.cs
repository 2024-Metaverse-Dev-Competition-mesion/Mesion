using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject itemPrefab;  // 생성할 아이템 프리팹
    public string itemName;        // 아이템 이름

    private GameObject currentItem;  // 현재 생성된 아이템을 참조하는 변수

    // 아이템을 현재 아이템의 위치로 생성하는 메서드
    public GameObject SpawnItemAtCurrentPosition()
    {
        // 기존 아이템이 있다면, 그 위치와 회전을 저장
        Vector3 spawnPosition = Vector3.zero;
        Quaternion spawnRotation = Quaternion.identity;

        if (currentItem != null)
        {
            spawnPosition = currentItem.transform.position;  // 기존 아이템의 위치
            spawnRotation = currentItem.transform.rotation;  // 기존 아이템의 회전

            Destroy(currentItem);  // 기존 아이템 제거
        }

        // 새로운 아이템을 기존 아이템의 위치와 회전에 맞춰 생성
        currentItem = Instantiate(itemPrefab, spawnPosition, spawnRotation);

        return currentItem;
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnButtonClick()
    {
        SpawnItemAtCurrentPosition();  // 기존 아이템의 위치로 새로운 아이템 생성
    }
}
