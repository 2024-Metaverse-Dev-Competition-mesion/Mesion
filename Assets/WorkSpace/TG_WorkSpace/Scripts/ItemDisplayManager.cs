using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayManager : MonoBehaviour
{
    public Text deviceNameText;      // 기기명을 표시하는 텍스트 UI
    public Text deviceTypeText;      // 기기 종류를 표시하는 텍스트 UI
    public Image deviceImageView;    // 기기 이미지를 표시하는 UI
    public Transform itemSpawnPoint; // 아이템을 생성할 위치 (예: 특정 지점)

    private GameObject currentActiveObject; // 현재 활성화된 오브젝트

    // 버튼을 눌렀을 때 호출될 함수로 아이템 정보를 받아옴
    public void UpdateItemDisplay(Item item)
    {
        // 기기명, 기기 종류, 이미지 UI 업데이트
        deviceNameText.text = item.itemName;
        deviceTypeText.text = item.deviceType;
        deviceImageView.sprite = item.deviceImage;

        // 현재 활성화된 오브젝트가 있으면 파괴
        if (currentActiveObject != null)
        {
            Destroy(currentActiveObject);
        }

        // 새로운 아이템을 해당 위치에 생성
        if (item.itemPrefab != null)
        {
            // itemSpawnPoint 위치에 새로운 프리팹 생성
            currentActiveObject = Instantiate(item.itemPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("No prefab assigned to the selected item.");
        }
    }
}
