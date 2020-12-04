using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //index for our current line of dialogue and an index for a set question marker of the dialogue 
    public int currentLineIndex, optionIndex;
    public Vector2 scr;
    public Player player;
    //mouselook script reference for the maincamera
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public new string name;
    //array for text for our dialogue
    public string[] dialogueText;
    #endregion
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnGUI()
    {
        if (showDlg)
        {
            //set up our ratio messurements for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, 3 * scr.y), name + " : " + dialogueText[currentLineIndex]);

            //if not at the end of the dialogue or not at the options index
            if (!(currentLineIndex >= dialogueText.Length - 1 || currentLineIndex == optionIndex))
            {
                //Next button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Next"))
                {
                    //move forward in our dialouge array
                    currentLineIndex++;
                }
            }
            //else if we are at options
            else if (currentLineIndex == optionIndex)
            {
                //Accept button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(14 * scr.x, 8.5f * scr.y, scr.x * 2, 0.5f * scr.y), "Accept"))
                {
                    //move forward in our dialouge array
                    currentLineIndex++;
                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(12 * scr.x, 8.5f * scr.y, scr.x * 2, 0.5f * scr.y), "Decline"))
                {
                    //skip to end of dlg;
                    currentLineIndex = dialogueText.Length - 1;
                }
            }
            //else we are at the end
            else
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                {
                    //close the dialogue box
                    showDlg = false;
                    //set index back to 0 
                    currentLineIndex = 0;

                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible       
                    Cursor.visible = false;
                }
            }
        }
    }
}
