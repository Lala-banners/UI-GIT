using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopStates
{
    open,
    closed
}

public class Shop : MonoBehaviour
{
    #region Shop Variables
    public List<ItemData> shopInventory = new List<ItemData>();
    private List<ItemData> inv;
    public ItemData selectedItem;
    private Inventory playerInv;
    public GameObject shop;
    public Button buy, sell;

    public GameObject shopPrefab;
    public Transform shopSlotParent;
    #endregion

    #region Inventory Display
    public TMP_Text itemText;
    public TMP_Text descriptionText;
    public TMP_Text amountText;
    public TMP_Text valueText;
    public Image icon;
    private GameObject mesh;
    #endregion

    #region Display Shop Variables
    [SerializeField] public bool showShop = false;
    private Vector2 scr;
    #endregion

    private void Awake()
    {
        //Add random items to shop
        shopInventory.Add(ItemData.CreateItem(Random.Range(0, 2)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(0, 2)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInventory.Add(ItemData.CreateItem(Random.Range(100, 102)));
    }

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
        DisplayShopInventory(selectedItem);


        #region Logic
        foreach (ItemData items in shopInventory)
        {
            selectedItem = items;
            GameObject itemSlot = Instantiate(shopPrefab, shopSlotParent);
            Button itemButton = itemSlot.GetComponent<Button>();
            selectedItem.button = itemButton;
            itemButton.onClick.AddListener(() => DisplayShopInventory(items));

            ShopSlot shopSlot = itemSlot.GetComponent<ShopSlot>();
            Image image = shopSlot.image;

            if(image != null)
            {
                image.sprite = items.Icon;
            }
        }
        #endregion
    }

    public void DisplayShopInventory(ItemData item)
    {
        #region Visuals
        selectedItem = item;
        itemText.text = selectedItem.Name.ToString();
        descriptionText.text = selectedItem.Description.ToString();
        amountText.text = selectedItem.Amount.ToString();
        valueText.text = selectedItem.Value.ToString();
        icon.sprite = selectedItem.Icon != null ? selectedItem.Icon : null;
        mesh = selectedItem.Mesh;
        #endregion

        buy.onClick.AddListener(() => BuyItem(1));
        sell.onClick.AddListener(() => SellItem(1));
    }

    public void BuyItem(int amount)
    {
        if(playerInv.money >= selectedItem.Value * amount)
        {
            playerInv.money -= selectedItem.Value * amount;
            for (int i = 0; i < amount; i++)
            {
                playerInv.AddItem(selectedItem);
            }
        }
    }

    public void SellItem(int amount)
    {
        if (playerInv.money <= selectedItem.Value * amount)
        {
            playerInv.money -= selectedItem.Value * amount;
            for (int i = 0; i < amount; i++)
            {
                inv.Remove(selectedItem);
            }
        }
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