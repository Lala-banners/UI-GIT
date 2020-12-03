using UnityEngine;
using UnityEngine.UI;

public class DialogueNPC : NPC
{
    public Text textDisplay;
    [SerializeField] protected string[] dialogueText;

    public override void Interact()
    {
        dialogue.name = npcName;
        dialogue.showDialogue = true;
        Debug.Log("Dialogue NPC");
    }
}
