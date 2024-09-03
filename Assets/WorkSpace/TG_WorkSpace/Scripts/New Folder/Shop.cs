using UnityEngine;

public class Shop : MonoBehaviour
{
    public Item itemForSale;

    void Start()
{
    // ScriptableObject로 아이템 생성
    itemForSale = ScriptableObject.CreateInstance<Item>();
    itemForSale.itemName = "Sword";
    itemForSale.description = "A sharp blade.";
    itemForSale.itemID = 1;
    itemForSale.price = 100;
}

    public void PurchaseItem()
    {
        if (GameManager_tg.Instance.SpendCurrency(itemForSale.price))
        {
            GameManager_tg.Instance.AddItem(itemForSale);
            Debug.Log("Purchased " + itemForSale.itemName);
        }
        else
        {
            Debug.Log("Not enough currency to purchase " + itemForSale.itemName);
        }
    }
}
