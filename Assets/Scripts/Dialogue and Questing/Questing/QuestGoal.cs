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
    Gather
}

[System.Serializable]
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;
    public GoalType goalType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;

    public Inventory inv;

    public ItemData item;

    public abstract bool isCompleted();
    
}
