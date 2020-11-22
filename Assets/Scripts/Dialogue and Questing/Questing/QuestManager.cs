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
    //For multiple quests: public Quest[] quests;

    public void AcceptQuest(Quest acceptedQuest)
    {
        currentQuest = acceptedQuest;
        currentQuest.goal.questState = QuestState.Active;
    }

    public void DeclineQuest()
    {
        //dialogue.dialogueText[2] = di
    }

    public void ClaimQuest()
    {
        if(currentQuest.goal.questState == QuestState.Completed && currentQuest.goal.isCompleted() == true)
        {
            //Add money
            inv.money += currentQuest.goldReward;

            //Add experience

            currentQuest.goal.questState = QuestState.Claimed;
            Debug.Log("Quest Claimed");
        }
    }
}
