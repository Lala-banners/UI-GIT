using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemId;
    public ItemType itemType;
    Inventory inv;
    public int amount;

    //Function for collecting inventory from the world
    public void CollectItems()
    {
        //If item type is money
        if(itemType == ItemType.Money)
        {
            inv.money += amount; //Add money to amount
        }
        //Else if type is weapon, apparel or quest then add item to inventory
        else if(itemType == ItemType.Weapon || itemType == ItemType.Apparel || itemType == ItemType.Quest)
        {
            inv.AddItem(inv.item); //add to inventory
        }
        else //if type is food, crafting, ingridients, potions or scrolls
        {
            int found = 0;
            int addIndex = 0;

            for (int i = 0; i < inv.inventory.Count; i++) //check the count of inventory in inventory
            {
                if (itemId == inv.inventory[i].ID)
                {
                    found = 1;
                    addIndex = i;
                    break;
                }
            }
            if (found == 1) //if found 1 item, add to amount
            {
                inv.inventory[addIndex].Amount += amount;
            }
            else 
            {

                for (int i = 0; i < inv.inventory.Count; i++)
                {
                    if (itemId == inv.inventory[i].ID)
                    {
                        inv.inventory[i].Amount = amount;
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}
    
