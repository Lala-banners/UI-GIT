using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TMP_Text itemText;

    #region Inventory Variables
    public List<Item> inventory = new List<Item>();
    [SerializeField] private Item selectedItem;
    public Item item;
    [SerializeField] private Player player;
    [SerializeField] public bool showInventory = false;
    public int money = 100;
    #endregion

    #region GUI Inventory Variables
    public GUIStyle[] Styles;
    private Vector2 scr; //screen
    private Vector2 scrollPos;
    private string sortType = "";
    #endregion

    #region Equipment
    [System.Serializable]
    public struct Equipment
    {
        public string slotName; //chest, feet, head etc
        public Transform equipLocation; //where the equipment will be set
        public GameObject currentItem;
        public Item item; //ref for check to make sure its not the same item
    };
    public Equipment[] equipmentSlots; //First slot head, second chest, third weapon etc
    #endregion

    

    private void Update()
    {
        
    }

    

    void UseItemGUI()
    {
        GUI.Box(new Rect(4.55f * scr.x, 3.5f * scr.y,
                        2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);

        //Icon
        GUI.Box(new Rect(4.25f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
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
                if(player.playerStats.CurrentHealth < player.playerStats.healthHearts.maximumHealth)
                {
                    if(GUI.Button(new Rect(4.5f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                    {
                        selectedItem.Amount--;
                        player.playerStats.Heal(selectedItem.Heal);
                        //player.stats.currentMana += selectedItem.healMana; 

                        if(selectedItem.Amount <= 0)
                        {
                            inventory.Remove(selectedItem);
                            selectedItem = null;
                            break;
                        }
                        print("Eat");
                    }
                }
                break;
            case ItemType.Weapon:
                if(equipmentSlots[2].currentItem == null || 
                    selectedItem.Name != equipmentSlots[2].item.Name) //If no weapon equipped, then equip selected weapon
                {
                    if(GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, 
                        scr.x, 0.25f * scr.y), "Equip"))
                    {
                        if(equipmentSlots[2].currentItem == null)
                        {
                            Destroy(equipmentSlots[2].currentItem);
                        }
                        GameObject currentItem = Instantiate(selectedItem.Mesh, equipmentSlots[2].equipLocation);
                        equipmentSlots[2].currentItem = currentItem;
                        equipmentSlots[2].item = selectedItem;
                    }
                }
                else //if item equipped is the same as eqiped item, then upequip the weapon
                {
                    if(GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, 
                        scr.x, 0.25f * scr.y), "Unequip"))
                    {
                        Destroy(equipmentSlots[2].currentItem);
                        equipmentSlots[2].item = null;
                    }
                }
                break;
            case ItemType.Apparel: //Change colour of armour
                //if()
                break;
            case ItemType.Crafting:
                //if()
                break;
            case ItemType.Ingredients:
                //if()
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
    }

    public void FindItem(string itemName)
    {
        Item foundItem = inventory.Find(findItem => findItem.Name == itemName);

        //return foundItem;
    }

    public void AddItem(Item item)
    {
        //inventory.Add(item);
        Item foundItem = inventory.Find(FindItem => FindItem.Name == item.Name);

        if(foundItem != null)
        {
            foundItem.Amount++;
            print("Found an item!");
        }
        else
        {
            Item newItem = new Item(item, 1);
            inventory.Add(item);
        }
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
                if (GUI.Button(new Rect(4 * scr.x + i * scr.x, 0, scr.x,0.25f * scr.y), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
            Display();
            if (selectedItem != null)
            {
                UseItemGUI();
            }
        }
    }
    }
    #endregion

