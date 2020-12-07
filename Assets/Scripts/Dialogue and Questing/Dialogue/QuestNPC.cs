using UnityEngine;

public class QuestNPC : DialogueNPC
{
    [SerializeField] protected QuestManager questManager;
    [SerializeField] protected Quest NPCsQuest;
    public string[] completeText; //complete quest dialogue

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
                dialogue.name = npcName;
                dialogue.dialogueText = completeText;
                dialogue.ShowDialogue();
                break;

        }
    }
}
