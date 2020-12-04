using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    [SerializeField] protected string npcName;
    public Dialogue dialogue;

    //Does something different depending on what type of NPC player is interacting with
    public abstract void Interact();
    
}
