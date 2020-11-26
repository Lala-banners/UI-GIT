using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNPC : NPC
{
    public Text textDisplay;
    [SerializeField] public Dialogue dialogue;
    [SerializeField] protected string[] dialogueText;

    public override void Interact()
    {
        dialogue.name = name;
        dialogue.showDialogue = true;
        Debug.Log("Dialogue NPC");
    }
}
