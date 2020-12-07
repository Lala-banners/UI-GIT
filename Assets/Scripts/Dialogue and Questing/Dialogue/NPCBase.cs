using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    [SerializeField] protected string npcName;
    public Dialogue dialogue;
    public static bool showDialogue;

    //Does something different depending on what type of NPC player is interacting with
    public abstract void Interact();
    
}
