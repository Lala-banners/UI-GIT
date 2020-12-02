using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Shop Variables
    public List<ItemData> shopInventory = new List<ItemData>();
    private ItemData selectedItem;
    private Inventory playerInv;
    #endregion

    #region Display Shop Variables
    [SerializeField] public bool showShop = false;
    private Vector2 scr;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        playerInv = (Inventory)FindObjectOfType<Inventory>(); 

        if(playerInv == null)
        {
            Debug.LogError("There is no player with an items in the scene");
        }
    }

    #region IMGUI
    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if(showShop)
        {
            for (int i = 0; i < shopInventory.Count; i++)
            {
                if(GUI.Button(new Rect(12.5f * scr.x,(0.25f * scr.y) + i * 
                             (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y),   
                             shopInventory[i].Name))
                {
                    selectedItem = shopInventory[i];
                }

                if(selectedItem != null)
                {
                    GUI.Box(new Rect(8.5f * scr.x, 0.25f * scr.y, 
                        3.5f * scr.x, 7 * scr.y), "");
                    GUI.Box(new Rect(8.75f * scr.x, 0.5f * scr.y, 
                        3 * scr.x, 3 * scr.y), selectedItem.Icon);
                    GUI.Box(new Rect(9.05f * scr.x, 3.5f * scr.y, 
                        2.5f * scr.x, 0.5f * scr.y), selectedItem.Name);
                    GUI.Box(new Rect(8.75f * scr.x, 4 * scr.y, 
                        3 * scr.x, 3 * scr.y), selectedItem.Description 
                        + "\nValue: " + selectedItem.Value 
                        + "\nAmount: " + selectedItem.Amount);

                    if(playerInv.money >= selectedItem.Value)
                    {
                        if(GUI.Button(new Rect(10.5f * scr.x, 6.5f * scr.y, 
                                        scr.x, 0.25f * scr.y), "Purchase ItemData"))
                        {
                            playerInv.money -= selectedItem.Value;

                            //Add to player
                            playerInv.AddItem(selectedItem);

                            //Remove from shop
                            selectedItem.Amount--;

                            if(selectedItem.Amount <= 0)
                            {
                                shopInventory.Remove(selectedItem);
                                selectedItem = null;
                            }
                        }
                    }
                }
                //Display Player Inventory
                playerInv.showInventory = true;

                if (GUI.Button(new Rect(0.25f * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Exit Shop")) 
                {
                    playerInv.showInventory = false;
                    showShop = false;
                }
            }
        }
    }
    #endregion
}
