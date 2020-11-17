using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
    //Requirements for Gather goal type
    private Inventory playerInv;
    public string itemName;
    public int requiredAmount;
    //public int currentAmount;

    private void Start()
    {
        playerInv = (Inventory)GameObject.FindObjectOfType<Inventory>();

        if (playerInv == null)
        {
            Debug.LogError("There is no player with inventory in the scene");
        }
    }

    private bool CheckPlayerInv()
    {
        if(playerInv == null)
        {
           return false;
            
        }
        return true;
    }

    //Each quest will have different way of being completed
    public override bool isCompleted()
    {
        if(CheckPlayerInv() == false)
        {
            return false;
        }

        //Item item = playerInv.FindItem(itemName);

       /* if(item == null)
        {
            return false;
        }

        if(item.Amount >= requiredAmount)
        {
            return true;
        }
       */
        return false;
    }
}
