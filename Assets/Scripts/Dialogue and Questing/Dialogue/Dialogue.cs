using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("References")]
    public bool showDialogue;
    public int currentLineIndex; //index for current line of dialogue

    public Vector2 scr;

    [Header("NPC Name and Dialogue")]
    public string name;
    public string[] dialogueText;
    //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line



    private void OnGUI()
    {
        if(showDialogue)
        {
            //set up ratio for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            GUI.Box(new Rect(0,6 * scr.y, Screen.width, scr.y * 3), name + " : " + dialogueText[currentLineIndex]);
            
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
                    if(GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                    {
                        showDialogue = false;
                        currentLineIndex = 0;
                    }
                }
            }
        }
    }
}
