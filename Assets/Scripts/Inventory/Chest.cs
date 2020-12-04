using System.Collections.Generic;//List
using UnityEngine;

public enum ChestStates
{
    open,
    closed
}

public class Chest : MonoBehaviour
{
    public List<ItemData> chestInv = new List<ItemData>();
    public List<ItemData> inventory = new List<ItemData>();
    public ItemData selectedItem;
    public bool showChestInv;
    public Vector2 scr;
    public GameObject chestObj;
    public GameObject invObject;

    private void Start()
    {
        Closed();
        chestInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        chestInv.Add(ItemData.CreateItem(Random.Range(100, 102)));
    }

    public ChestStates states;

    public void Open()
    {
        states = ChestStates.open;
        //showChestInv = true;
        chestObj.SetActive(true);
        invObject.SetActive(true);
    }
    
    public void Closed()
    {
        states = ChestStates.closed; 
        //showChestInv = false;
        chestObj.SetActive(false);
        invObject.SetActive(false);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other) //Make chest open when player triggers chest
    {   
        if(other.gameObject.tag == "Player")
        {
            Open();
            Debug.Log("Chest has been opened");
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        Closed();
        Debug.Log("Chest is closed");
    }

    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        if (showChestInv)
        {
            //Display of the chest items
            for (int i = 0; i < chestInv.Count; i++)
            {
                if (GUI.Button(new Rect(12.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), chestInv[i].Name))
                {
                    selectedItem = chestInv[i];
                }
            }
            if (selectedItem != null)
            {
                GUI.Box(new Rect(8.5f * scr.x, 0.25f * scr.y, 3.5f * scr.x, 7 * scr.y), "");
                GUI.Box(new Rect(8.75f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon.texture);
                GUI.Box(new Rect(9.05f * scr.x, 3.5f * scr.y, 2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);
                GUI.Box(new Rect(8.75f * scr.x, 4f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);

                if (GUI.Button(new Rect(10.5f * scr.x, 6.75f * scr.y, scr.x, 0.25f * scr.y), "Take Item"))
                {
                    //add to player
                    inventory.Add(ItemData.CreateItem(selectedItem.ID));

                    //remove from chest
                    chestInv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
    }
}
