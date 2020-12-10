using System.Collections.Generic;
using UnityEngine;

public enum ShopStates
{
    open,
    closed
}

public class Shop : MonoBehaviour
{
    #region Shop Variables
    public List<ItemData> shopInventory = new List<ItemData>();
    private ItemData selectedItem;
    private Inventory playerInv;
    public GameObject shop;


    public GameObject shopPrefab;
    public Transform shopSlotParent;
    #endregion

    #region Display Shop Variables
    [SerializeField] public bool showShop = false;
    private Vector2 scr;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        playerInv = (Inventory)FindObjectOfType<Inventory>();

        if (playerInv == null)
        {
            Debug.LogError("There is no player with an inventory in the scene");
        }

        Closed();
    }

    public ShopStates states;

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Open();
            Debug.Log("Shop has been opened");
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        Closed();
    }


    public void Open()
    {
        states = ShopStates.open;
        //showChestInv = true;
        playerInv.ActivateInventory();
        shop.SetActive(true);
    }

    public void Closed()
    {
        states = ShopStates.closed;
        //showChestInv = false;
        shop.SetActive(false);
    }

    #region IMGUI
    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (showShop)
        {
            //Display of the shop items
            for (int i = 0; i < shopInventory.Count; i++)
            {
                if (GUI.Button(new Rect(12.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), shopInventory[i].Name))
                {
                    selectedItem = shopInventory[i];
                }
            }
            if (selectedItem != null)
            {
                GUI.Box(new Rect(8.5f * scr.x, 0.25f * scr.y, 3.5f * scr.x, 7 * scr.y), "");
                GUI.Box(new Rect(8.75f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon.texture);
                GUI.Box(new Rect(9.05f * scr.x, 3.5f * scr.y, 2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);
                GUI.Box(new Rect(8.75f * scr.x, 4f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                if (playerInv.money >= selectedItem.Value)
                {
                    if (GUI.Button(new Rect(10.5f * scr.x, 6.75f * scr.y, scr.x, 0.25f * scr.y), "Buy Item"))
                    {
                        playerInv.money -= selectedItem.Value;
                        //add to player
                        playerInv.inventory.Add(ItemData.CreateItem(selectedItem.ID));
                        //remove from shop
                        shopInventory.Remove(selectedItem);
                        selectedItem = null;
                        return;
                    }
                }

            }
            if (GUI.Button(new Rect(0.25f * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Exit Shop"))
            {
                showShop = false;
                playerInv.showInventory = false;
                playerInv.currentShop = null;
            }
            GUI.Box(new Rect(7f * scr.x, 8.5f * scr.y, 2 * scr.x, 0.5f * scr.y), "Money $" + playerInv.money);

        }
    }
}

#endregion