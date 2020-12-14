using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueNPC : NPCBase
{
    public TMP_Text textDisplay, nameDisplay;
    protected int dialogueIndex;
    public Button[] dialogueButtons;
    public GameObject dialogueTextBox;

    public override void Interact()
    {
        dialogueTextBox.SetActive(true);
        nameDisplay.text = npcName;
        textDisplay.text = dialogueText[dialogueIndex];
    }

    public void Next()
    {
        dialogueIndex++;

        if (dialogueIndex >= dialogueText.Length)
        {
            Bye();
        }
        textDisplay.text = dialogueText[dialogueIndex];
    }

    public void Bye()
    {
        dialogueIndex = 0;
        showDialogue = false;
        textDisplay.text = dialogueText[dialogueIndex];
    }

    public void NegativeResponse()
    {
        approval.approvalRate--;
    }

    public void PositiveResponse()
    {
        approval.approvalRate++;
    }
}
