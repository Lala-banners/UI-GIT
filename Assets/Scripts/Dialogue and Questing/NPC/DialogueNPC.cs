using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : NPC
{
    [SerializeField] public Dialogue dialogue;

    [SerializeField] protected string[] dialogueText;

    public override void Interact()
    {
        dialogue.name = name;
        //dialogue.dialogueText
        //dialogue.showDialogue = true;
        Debug.Log("Dialogue NPC");
    }
}
