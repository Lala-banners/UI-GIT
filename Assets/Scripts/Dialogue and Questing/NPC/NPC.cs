using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NPC : MonoBehaviour
{
    [SerializeField] protected string name;

    //Does something different depending on what type of NPC player is interacting with
    public abstract void Interact();
    
}
