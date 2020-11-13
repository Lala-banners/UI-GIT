using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Available,
    Active,
    Completed,
    Claimed
}

public enum GoalType
{
    Gather,
    //Kill,
    //Escort,
    //Locate
}

[System.Serializable]
public class QuestGoal 
{
    public QuestState questState;

    public GoalType goalType;

    //public Inventory inv;

    //Requirements for Gather goal type
    public int itemId;
    public int requiredAmount;
    public int currentAmount;

    public void ItemCollected(int ID)
    {
        if(goalType == GoalType.Gather && ID == itemId)
        {
            currentAmount++;

            if(currentAmount >= requiredAmount)
            {
                questState = QuestState.Completed;
                Debug.Log("Quest Complete");
            }
        }
    }
}
