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
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;

    public GoalType goalType;

    public Inventory inv;

    Item item;

    public abstract bool isCompleted();

    public void ItemCollected(string name, float requiredAmount)
    {
        if(goalType == GoalType.Gather && item.Name == name)
        {
            item.Amount++;

            if(item.Amount >= requiredAmount)
            {
                questState = QuestState.Completed;
                Debug.Log("Quest Complete");
            }
        }
    }
}
