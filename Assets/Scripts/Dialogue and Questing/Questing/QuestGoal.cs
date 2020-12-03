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

    public Inventory inv;

    ItemData item;

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
