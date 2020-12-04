using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //TODO : Lara : Dialogue -> talk to NPC, approval system based on dialogue choice (dislike, neutral, like), GUI & UI.
    [Header("References")]
    public bool showDialogue;
    public int currentLineIndex, optionIndex; //index for current line of dialogue
    //index for our current line of dialogue and an index for a set question marker of the dialogue 
  
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public new string name;
    //array for text for our dialogue
    public string[] dialogueText;

    public Player player;
    //public ThirdPersonMovement playerMovement;
    public Vector2 scr;

    public void ShowDialogue()
    {
        if(showDialogue)
        {
            //playerMovement.enabled = false;
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            showDialogue = true;
            player.Interact();
        }
    }

    private void OnGUI()
    {
        if(showDialogue)
        {
            //playerMovement.enabled = false;

            //set up ratio for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, scr.y * 3), name + " : " + dialogueText[currentLineIndex]);
            
            //if not at end of dialogue
            if(currentLineIndex < dialogueText.Length - 1)
            {
                //next button allows to skip forward to next line of dialogue
                if(GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Next"))
                {
                    currentLineIndex++;
                }
                else
                {
                    if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                    {
                        showDialogue = false;
                        currentLineIndex = 0;
                        //playerMovement.enabled = true;
                    }
                }
            }
        }
    }
}
