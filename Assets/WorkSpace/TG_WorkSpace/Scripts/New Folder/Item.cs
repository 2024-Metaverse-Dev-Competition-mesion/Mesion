using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public int itemID;
    public int price;

    public void Purchase()
    {
        Debug.Log(itemName + " has been purchased.");
    }
}