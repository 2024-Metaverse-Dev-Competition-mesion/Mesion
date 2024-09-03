using UnityEngine;

public class DisplayShelfInteraction : MonoBehaviour
{
    public GameObject uiPanel;  // UI 패널 참조 (유니티 에디터에서 설정)
    private Item currentItem;  // 현재 상호작용 중인 아이템
    private bool isPlayerNearby = false;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);  // 초기에는 UI 패널 비활성화
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            CheckForClosestItem();  // 가장 가까운 아이템을 확인하여 UI를 표시
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);  // 플레이어가 멀어지면 UI 패널 비활성화
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            CheckForClosestItem();  // 지속적으로 가장 가까운 아이템을 확인
        }
    }

    void CheckForClosestItem()
    {
        float closestDistance = Mathf.Infinity;
        Item closestItem = null;

        foreach (Transform itemTransform in transform)  // 진열매대의 모든 자식 오브젝트 확인
        {
            Item item = itemTransform.GetComponent<Item>();
            if (item != null)
            {
                float distance = Vector3.Distance(itemTransform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (closestItem != null && closestItem != currentItem)
        {
            currentItem = closestItem;
            ShowItemInfo();  // UI 패널 활성화
        }
    }

    void ShowItemInfo()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);  // UI 패널 활성화
        }
    }

    public void PurchaseItem()
    {
        if (currentItem != null)
        {
            if (GameManager_tg.Instance.SpendCurrency(currentItem.price))
            {
                GameManager_tg.Instance.AddItem(currentItem);
                currentItem.Purchase();  // 아이템 구매 로직 호출
                if (uiPanel != null)
                {
                    uiPanel.SetActive(false);  // 구매 후 UI 패널 비활성화
                }
            }
            else
            {
                Debug.Log("Not enough currency to purchase " + currentItem.itemName);
            }
        }
    }
}
