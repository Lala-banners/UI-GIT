﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    #region Inventory Variables
    public List<Item> inventory = new List<Item>();
    [SerializeField] private Item selectedItem;
    #endregion

    #region Display Inv Vars
    public GUIStyle[] Styles;
    [SerializeField] private bool showInventory = false;
    private Vector2 scr; //screen
    private Vector2 scrollPos;
    private string sortType = "";
    #endregion

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
    
    void UseItem()
    {
        GUI.Box(new Rect(4.55f * scr.x, 3.5f * scr.y,
                            2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);

        //description, value, amount

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

        //Style
        switch (selectedItem.Type)
        {
            case ItemType.Food:
                break;
            case ItemType.Weapon:
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
    }

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
                UseItem();
            }
        }
    }
}