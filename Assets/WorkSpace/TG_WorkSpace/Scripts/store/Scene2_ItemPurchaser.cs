using UnityEngine;
using UnityEngine.UI;

public class Scene2_ItemPurchaser : MonoBehaviour
{
    public Button purchaseButton;  // ���� ��ư
    public Item itemToBuy;  // ������ ������ (�����Ϳ��� ���� ����)
    public int itemPrice = 500;  // ������ ����
    public GameObject itemGameObject;  // ��Ȱ��ȭ�� �������� ���� ������Ʈ

    private bool isPurchased = false;  // ������ ���� ���� Ȯ�� ����

    void Start()
    {
        purchaseButton.onClick.AddListener(TryPurchaseItem);  // ��ư Ŭ�� ������ �߰�
    }

    void TryPurchaseItem()
    {
        if (!isPurchased)  // �̹� ������ ���°� �ƴ϶��
        {
            if (GameManager_tg.Instance.SpendCurrency(itemPrice))  // ���� ������� Ȯ���ϰ� ����
            {
                GameManager_tg.Instance.AddItem(itemToBuy);  // �������� �κ��丮�� �߰�
                Debug.Log("Scene2: Purchased " + itemToBuy.itemName + ". Remaining currency: " + GameManager_tg.Instance.currentCurrency);

                // ���� �Ϸ� ���·� ����
                isPurchased = true;

                // UI�� ������Ʈ ��Ȱ��ȭ
                purchaseButton.interactable = false;  // ���� ��ư ��Ȱ��ȭ
                if (itemGameObject != null)
                {
                    itemGameObject.SetActive(false);  // �ش� �������� ������Ʈ ��Ȱ��ȭ
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
