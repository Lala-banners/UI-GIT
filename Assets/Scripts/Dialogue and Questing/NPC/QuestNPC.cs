using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    [SerializeField] protected QuestManager questManager;
    [SerializeField] protected Quest NPCsQuest;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();

        if(questManager == null)
        {
            Debug.LogError("There is no Quest Manager in the scene");
        }
    }

    //Depending on type of quest, interact differently with that quest NPC
    public override void Interact()
    {
        switch(NPCsQuest.goal.questState)
        {
            case QuestState.Available:
            questManager.AcceptQuest(NPCsQuest);
                Debug.Log("Quest Accepted");
                break;
            case QuestState.Active:
                if(NPCsQuest.goal.isCompleted())
                {
                    Debug.Log("Quest Completed!");
                    questManager.ClaimQuest();
                }
                else
                {
                    Debug.Log("Quest not claimed");
                }
                break;
            case QuestState.Claimed:
                
                //some dialogue 
                //"You have already completed this quest, bugger off"
                break;

        }
    }
}
