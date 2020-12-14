using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest curQuest;
    public Player player;
    public Inventory inv;
    public GameObject questWindow;
    public TMP_Text titleText, descriptionText, experienceText, goldText;
    public Dialogue dialogue;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = curQuest.title;
        descriptionText.text = curQuest.description;
        experienceText.text = curQuest.experienceReward.ToString();
        goldText.text = curQuest.goldReward.ToString();
    }

    public void AcceptQuest(Quest acceptedQuest)
    {
        curQuest = acceptedQuest;
        questWindow.SetActive(false);
        curQuest.goal.questState = QuestState.Active;
    }

    public void DeclineQuest()
    {
        
    }

    public void ClaimQuest()
    {
        if(curQuest.goal.questState == QuestState.Completed && curQuest.goal.isCompleted() == true)
        {
            //Add money
            inv.money += curQuest.goldReward;

            curQuest.goal.questState = QuestState.Claimed;
            Debug.Log("Quest Claimed");
        }
    }
}
