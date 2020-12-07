using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public PauseHandler pause;

    #region Inventory Variables
    [Header("Inventory References")]
    public List<ItemData> inventory = new List<ItemData>(); //List of inventory
    [SerializeField] private ItemData selectedItem;
    public ItemData currentItem;
    public GameObject slotPrefab;
    public Transform inventorySlotParent;
    public Button use, discard, move; //references to use and discard buttons

    [SerializeField] private Player player;
    [SerializeField] public bool showInventory = false;
    public Chest currentChest;
    public Shop currentShop;

    [Header("Inventory Variables")]
    public string[] enumTypesForItems;
    public int money = 100;
    public TMP_Text itemText;
    public TMP_Text descriptionText;
    public TMP_Text amountText;
    public TMP_Text valueText;
    public Image icon;
    private GameObject mesh;
    public Transform dropLocation;
    public GameObject itemPrefab;
    public GameObject axePrefab, swordPrefab, shieldPrefab;

    public GameObject itemSelectedPanel;
    #endregion

    #region GUI Inventory Variables
    private GUIStyle[] Styles;
    private Vector2 scr; //screen
    private Vector2 scrollPos;
    private string sortType = "";
    #endregion

    //TODO : Lara : Convert all IMGUI to Canvas! (Inventory, shop, money, quests, dialogue etc)



    private void Start()
    {
        enumTypesForItems = new string[] { "All", " Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potions", "Scrolls", "Quest" };
    }

    //For eating apples, using potions etc
    public void UseItem()
    {
        #region Heal with potion
        if (player.playerStats.CurrentHealth < player.playerStats.healthHearts.maximumHealth)
        {
            if(selectedItem.Amount > 0 )
            {
                player.playerStats.Heal(25);
            }
            selectedItem.Amount--;
            Debug.Log("No more health potions in inventory");
            

            if (selectedItem.Amount <= 0)
            {
                inventory.Remove(selectedItem);
                Destroy(selectedItem.button.gameObject);
                itemSelectedPanel.SetActive(false);
            }
            else
            {
                DisplayItem(selectedItem);
            }
        }
        #endregion
    }

    public void DisplayItem(ItemData item) //Display in Inventory
    {
        selectedItem = item;
        itemSelectedPanel.SetActive(true);

        //Display information from ItemData
        itemText.text = selectedItem.Name.ToString();
        descriptionText.text = selectedItem.Description.ToString();
        amountText.text = selectedItem.Amount.ToString();
        valueText.text = selectedItem.Value.ToString();
        icon.sprite = selectedItem.Icon != null ? selectedItem.Icon : null;
        mesh = selectedItem.Mesh;

        use.onClick.AddListener(UseItem);
        discard.onClick.AddListener(DiscardItem);
        move.onClick.AddListener(MoveItem);

    }

    public void MoveItem() //Move to chest or shop
    {
        //Move from inventory to chest
        selectedItem.Amount--;
        inventory.Remove(selectedItem);
        currentChest.chestInv.Add(selectedItem);
        Destroy(selectedItem.button);
        print("Item moved from Inventory");
    }

    public void DiscardItem() //THIS WORKS!
    {
        GameObject droppedItem = Instantiate(itemPrefab, dropLocation.position, Quaternion.identity); //instantiate item at drop location
        droppedItem.name = selectedItem.Name;
        inventory.Remove(selectedItem); //removes from inventory
    }

    public void EquipWeapon()
    {
        if (equipmentSlots[2].currentItem == null || selectedItem.Name != equipmentSlots[2].item.Name) //If no weapon equipped, then equip selected weapon
        {
            Destroy(equipmentSlots[2].currentItem);

            if (equipmentSlots[2].currentItem != null || selectedItem.Name == equipmentSlots[2].item.Name)
            {
                //equipmentSlots[2].item.Mesh
                equipmentSlots[2].currentItem = Instantiate<GameObject>(axePrefab, equipmentSlots[2].equipLocation); ;
                equipmentSlots[2].item = selectedItem;
            }
        }
        else //if item equipped is the same as equipped item, then upequip the weapon
        {
            Destroy(equipmentSlots[2].currentItem);
            equipmentSlots[2].item = null;
        }
    }

    public void ActivateInventory()
    {
        if (!pause.inventory.activeSelf)
        {
            pause.Paused(pause.inventory);

            Button[] allChildren = inventorySlotParent.GetComponentsInChildren<Button>();

            for (int x = allChildren.Length - 1; x >= 0; x--)
            {
                Destroy(allChildren[x].gameObject);
            }

            foreach (ItemData item in inventory)
            {
                //Inventory Button
                GameObject itemSlot = Instantiate(slotPrefab, inventorySlotParent); //Clone item at item slot
                Button itemButton = itemSlot.GetComponent<Button>();

                selectedItem = item;
                selectedItem.button = itemButton;

                itemButton.onClick.AddListener(() => DisplayItem(item));

                SlotImage slotImage = itemSlot.GetComponent<SlotImage>();
                Image image = slotImage.image;

                if (image != null)
                {
                    image.sprite = item.Icon;
                }
            }
        }
        else
        {
            pause.UnPaused();
            pause.inventory.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateInventory();
        }
    }

    /* void ThisHasPara(int x)
    {

    }*/

    #region Equipment
    [System.Serializable]
    public struct Equipment
    {
        public string slotName; //chest, feet, head etc
        public Transform equipLocation; //where the equipment will be set
        public GameObject currentItem;
        public ItemData item; //ref for check to make sure its not the same item
    };
    public Equipment[] equipmentSlots; //First slot head, second chest, third weapon etc
    #endregion

    //TODO : Lara : 1 -> Buy item

    void UseItemGUI()
    {
        GUI.Box(new Rect(4.55f * scr.x, 3.5f * scr.y,
                        2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);

        //Icon
        GUI.Box(new Rect(4.25f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon.texture);
        GUI.Box(new Rect(4.55f * scr.x, 3.5f * scr.y,
                         2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);
        GUI.Box(new Rect(4.25f * scr.x, 4 * scr.y,
                         3 * scr.x,
                         3 * scr.y),
                         selectedItem.Description +
                         "\nValue: " + selectedItem.Value +
                         "\nAmount: " + selectedItem.Amount);

        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (player.playerStats.CurrentHealth < player.playerStats.healthHearts.maximumHealth)
                {
                    if (GUI.Button(new Rect(4.5f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                    {
                        selectedItem.Amount--;
                        player.playerStats.Heal(selectedItem.Heal);

                        if (selectedItem.Amount <= 0)
                        {
                            inventory.Remove(selectedItem);
                            selectedItem = null;
                            break;
                        }
                    }
                }
                break;
            case ItemType.Weapon:
                if (equipmentSlots[2].currentItem == null ||
                    selectedItem.Name != equipmentSlots[2].item.Name) //If no weapon equipped, then equip selected weapon
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y,
                        scr.x, 0.25f * scr.y), "Equip"))
                    {
                        if (equipmentSlots[2].currentItem == null)
                        {
                            Destroy(equipmentSlots[2].currentItem);
                        }
                        GameObject currentItem = Instantiate(selectedItem.Mesh, equipmentSlots[2].equipLocation);
                        equipmentSlots[2].currentItem = currentItem;
                        equipmentSlots[2].item = selectedItem;
                    }
                }
                else //if item equipped is the same as equipped item, then upequip the weapon
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y,
                        scr.x, 0.25f * scr.y), "Unequip"))
                    {
                        Destroy(equipmentSlots[2].currentItem);
                        equipmentSlots[2].item = null;
                    }
                }
                break;
            case ItemType.Apparel:
                break;
            case ItemType.Crafting:
                break;
            case ItemType.Ingredients:
                break;
            case ItemType.Potions:
                break;
            case ItemType.Scrolls:
                break;
            case ItemType.Quest:
                break;
            case ItemType.Money:
                break;
            default:
                break;
        }

        if (GUI.Button(new Rect(5.25f * scr.x, 6.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
        {
            /*GameObject droppedItem = Instantiate(itemPrefab, dropLocation.position, Quaternion.identity); //instantiate item at drop location
            droppedItem.name = selectedItem.Name;
            inventory.Remove(selectedItem); //removes from inventory*/
        }
    }

    public void FindItem(string itemName)
    {
        ItemData foundItem = inventory.Find(findItem => findItem.Name == itemName);
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(item);
        ItemData foundItem = inventory.Find(FindItem => FindItem.Name == item.Name);
    }

    #region OnGUI Display
    private void Display()
    {
        if (sortType == "")
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, inventory.Count * .25f * scr.y), false, true);
                if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                }
                GUI.EndScrollView();
            }
        }
        else
        {
            //if not empty, display sort type
            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), sortType);
            int slotCount = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type == type)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + slotCount * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y), inventory[i].Name))
                    {
                        selectedItem = inventory[i];
                    }
                    slotCount++;
                }
            }
        }
    }
    #endregion

    #region void OnGUI
    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;


        if (showInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            string[] itemTypes = Enum.GetNames(typeof(ItemType));
            int CountOfItemTypes = itemTypes.Length;

            for (int i = 0; i < CountOfItemTypes; i++)
            {
                if (GUI.Button(new Rect(4 * scr.x + i * scr.x, 0, scr.x, 0.25f * scr.y), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
            Display();
            if (selectedItem != null)
            {
                //UseItemGUI();
            }
        }
    }
}
#endregion

