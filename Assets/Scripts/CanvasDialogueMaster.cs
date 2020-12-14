using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CanvasDialogueMaster : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] currentDialogue;
    public int dialogueIndex;
    

    [Header("NPC and Player")]
    public string characterNPCName;
    //public ThirdPersonMovement playerMovement;

    [Header("Quest")]
    public Button questButton; //Accept quest button
    public TMP_Text buttonText;
    public QuestManager currentQuest;
    public GameObject questWindow;


    private void Start()
    {
        //playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
    }

    public void SetUp()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = characterNPCName + ": " + currentDialogue[dialogueIndex];
        buttonText.text = "Accept";

        if (currentQuest.curQuest.goal.questState == QuestState.Completed || currentQuest.curQuest.goal.questState == QuestState.Available)
        {
            questButton.interactable = true;
        }
        else
        {
            questButton.interactable = false;
        }
    }

    public void ButtonInteraction()
    {
        if (!(dialogueIndex >= currentDialogue.Length - 1))
        {
            dialogueIndex++;
            if (dialogueIndex >= currentDialogue.Length - 1)
            {
                buttonText.text = "Decline";
            }
        }
        else
        {
            dialogueIndex = 0;
            Camera.main.GetComponent<ThirdPersonMovement>().enabled = true;
            //playerMovement.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //dialoguePanel.SetActive(false);
        }
        dialogueText.text = characterNPCName + ": " + currentDialogue[dialogueIndex];
    }
}
