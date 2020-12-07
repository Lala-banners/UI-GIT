using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueNPC : NPCBase
{
    public TMP_Text textDisplay, nameDisplay;
    [SerializeField] protected string[] dialogueText;
    protected int dialogueIndex;
    public Button[] dialogueButtons;

    public override void Interact()
    {
        nameDisplay.text = npcName;
        showDialogue = true;
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
}
