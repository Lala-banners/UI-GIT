using UnityEngine;

[System.Serializable]
public class Quest 
{
    [Header("Quest Requirements")]
    public string title;
    public string description;
    public QuestNPC questGiver;
    public QuestGoal goal;


    [Header("Rewards")]
    public int experienceReward;
    public int goldReward;

}
