using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest quest;
    public Player player;
    public Inventory inv;
    public GameObject questWindow;
    public TMP_Text titleText, descriptionText, experienceText, goldText;

    Dialogue dialogue;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void AcceptQuest(Quest acceptedQuest)
    {
        quest = acceptedQuest;
        questWindow.SetActive(false);
        quest.goal.questState = QuestState.Active;
    }

    public void DeclineQuest()
    {
        //dialogue.dialogueText[2] = di
    }

    public void ClaimQuest()
    {
        if(quest.goal.questState == QuestState.Completed && quest.goal.isCompleted() == true)
        {
            //Add money
            inv.money += quest.goldReward;

            //Add experience

            quest.goal.questState = QuestState.Claimed;
            Debug.Log("Quest Claimed");
        }
    }
}
