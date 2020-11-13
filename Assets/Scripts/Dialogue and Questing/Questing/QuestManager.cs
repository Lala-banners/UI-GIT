using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Player player;
    public Inventory inv;

    Dialogue dialogue;

    //QuestGiver
    public Quest currentQuest;
    //For multiple quests 
    //public Quest[] quests;

    public void AcceptQuest()
    {

    }

    public void DeclineQuest()
    {

    }

    public void ClaimQuest()
    {
        if(currentQuest.goal.questState == QuestState.Completed)
        {
            //Add money

            //Add experience

            currentQuest.goal.questState = QuestState.Claimed;
            Debug.Log("Quest Claimed");
        }
    }
}
