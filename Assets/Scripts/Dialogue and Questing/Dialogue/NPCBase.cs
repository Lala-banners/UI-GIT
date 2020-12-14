using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    public string npcName;
    public string[] dialogueText;
    public Dialogue dialogue;
    public static bool showDialogue;
    public Approval approval;

    //Does something different depending on what type of NPC player is interacting with
    public abstract void Interact();
    
}
