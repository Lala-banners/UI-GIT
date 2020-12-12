using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesBar : MonoBehaviour
{
    public List<ItemData> consumables = new List<ItemData>(); //list of items
                                                              //Display items
                                                              //Make use items on pressing numbers on keyboard?
                                                              //Cooldown thing - try waitforseconds and make button not interactable for 2 seconds?

    [Header("Consumable References")]
    public ItemData selectedItem;
    public GameObject consumablesPanel;
    public Image currentIcon;
    private GameObject mesh;
    public Inventory inventory;
    public Button slot1, slot2, slot3, slot4, slot5; //UI buttons
    public Transform consumeSlotParent; //location to instantiate data
    public GameObject consumeSlotPrefab; //prefab of slots


    /*
     * Players can add items to a Consumables bar
     * Items have cooldowns that visually show when items are available to use
     * Consumable items influence the characters health, mana or stamina
     * Items on the Consumables bar are tied to a hotkey for use
     */
    //Make slot buttons
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ActivateConsumableBar();
    }

    public void DisplayConsumableItem(ItemData item) //Display in consumables bar
    {
        //Filtering out items that can be displayed as only of type food or potions
        if (selectedItem.Type == ItemType.Food || selectedItem.Type == ItemType.Potions)
        {
            selectedItem = item;
            selectedItem.Icon = slot1.image.sprite; //making icon of selected item into slot 1
        }
        
    }

    //Display items in consumables bar
    public void ActivateConsumableBar()
    {
        Button[] allChildren = consumeSlotParent.GetComponentsInChildren<Button>();

        for (int x = allChildren.Length - 6; x >= 0; x--)
        {
            Destroy(allChildren[x].gameObject);
        }

        foreach (ItemData item in consumables)
        {
            //Consumable Bar Buttons
            GameObject itemSlot = Instantiate(consumeSlotPrefab, consumeSlotParent); //Clone item at item slot
            Button itemButton = itemSlot.GetComponent<Button>();

            selectedItem = item;
            slot1 = itemButton;

            itemButton.onClick.AddListener(() => DisplayConsumableItem(item));

            ConsumeSlot consumeSlot = itemSlot.GetComponent<ConsumeSlot>();
            Image image = consumeSlot.image;

            if(image != null)
            {
                image.sprite = slot1.image.sprite;
            }
        }
    }

    public void AddConsumables(ItemData item)
    {
        item = selectedItem;
        consumables.Add(item);
        DisplayConsumableItem(item);
    }

    //Function for setting cooldown once item has been used
    public void Cooldown()
    {

    }
}
