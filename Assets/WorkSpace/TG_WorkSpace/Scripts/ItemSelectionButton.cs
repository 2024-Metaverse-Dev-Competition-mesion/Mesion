using UnityEngine;

public class ItemSelectionButton : MonoBehaviour
{
    public Item itemToDisplay; // 이 버튼에서 보여줄 아이템 참조
    public ItemDisplayManager displayManager; // ItemDisplayManager 참조

    // 버튼 클릭 시 호출되는 메서드
    public void OnButtonClick()
    {
        // 선택된 아이템 정보를 UI와 오브젝트에 업데이트
        if (itemToDisplay != null && displayManager != null)
        {
            displayManager.UpdateItemDisplay(itemToDisplay);
        }
        else
        {
            Debug.LogError("Item or Display Manager is not assigned.");
        }
    }
}
