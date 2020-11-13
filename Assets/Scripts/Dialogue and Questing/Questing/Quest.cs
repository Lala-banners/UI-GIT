using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    [Header("Quest Requirements")]
    public string title;
    public string description;

    [Header("Rewards")] 
    public int experienceReward;
    public int goldReward;

    [Header("Player")]
    public int requiredLevel;

    public QuestGoal goal; //if wanted multiple goals, make array []


}
