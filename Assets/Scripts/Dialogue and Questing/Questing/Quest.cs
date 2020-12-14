using UnityEngine;

[System.Serializable]
public class Quest 
{
    [Header("Quest Requirements")]
    public string title;
    public string description;
    public QuestNPC questGiver;
    public QuestGoal goal;
    public GoalType type;

    [Header("Rewards")] 
    public int experienceReward;
    public int goldReward;

    [Header("Player")]
    public int requiredLevel;


}
