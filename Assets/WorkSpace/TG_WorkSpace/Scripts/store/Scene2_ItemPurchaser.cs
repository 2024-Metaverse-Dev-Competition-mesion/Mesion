using UnityEngine;
using UnityEngine.UI;

public class Scene2_ItemPurchaser : MonoBehaviour
{
    public Button purchaseButton;  // 구매 버튼
    public Item itemToBuy;  // 구매할 아이템 (에디터에서 설정 가능)
    public int itemPrice = 500;  // 아이템 가격
    public GameObject itemGameObject;  // 비활성화할 아이템의 게임 오브젝트

    private bool isPurchased = false;  // 아이템 구매 여부 확인 변수

    void Start()
    {
        purchaseButton.onClick.AddListener(TryPurchaseItem);  // 버튼 클릭 리스너 추가
    }

    void TryPurchaseItem()
    {
        if (!isPurchased)  // 이미 구매한 상태가 아니라면
        {
            if (GameManager_tg.Instance.SpendCurrency(itemPrice))  // 돈이 충분한지 확인하고 차감
            {
                GameManager_tg.Instance.AddItem(itemToBuy);  // 아이템을 인벤토리에 추가
                Debug.Log("Scene2: Purchased " + itemToBuy.itemName + ". Remaining currency: " + GameManager_tg.Instance.currentCurrency);

                // 구매 완료 상태로 설정
                isPurchased = true;

                // UI와 오브젝트 비활성화
                purchaseButton.interactable = false;  // 구매 버튼 비활성화
                if (itemGameObject != null)
                {
                    itemGameObject.SetActive(false);  // 해당 아이템의 오브젝트 비활성화
                }
            }
            else
            {
                Debug.Log("Scene2: Not enough currency to purchase " + itemToBuy.itemName + ". Current currency: " + GameManager_tg.Instance.currentCurrency);
            }
        }
        else
        {
            Debug.Log("Scene2: " + itemToBuy.itemName + " is already purchased.");
        }
    }
}
