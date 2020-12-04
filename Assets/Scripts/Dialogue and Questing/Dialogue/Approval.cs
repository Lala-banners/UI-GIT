using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approval : MonoBehaviour
{
    #region Approval Variables
    public string[] dislikeText, neutralText, likeText;
    public int approvalRate;
    public string response1, response2; //response of player (yes or no)
    #endregion

    #region Dialogue Variables
    public bool showDialogue;
    public int currentLineIndex, optionIndex;
    public Vector2 scr;
    public string[] dialogueText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (showDialogue)
        {
            //set up our ratio messurements for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, 3 * scr.y), name + " : " + dialogueText[currentLineIndex]);
            if (approvalRate <= -1)
            {
                dialogueText = dislikeText;
            }
            if (approvalRate == 0)
            {
                dialogueText = neutralText;
            }
            if (approvalRate >= 1)
            {
                dialogueText = likeText;
            }
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
                if (GUI.Button(new Rect(14 * scr.x, 8.5f * scr.y, scr.x * 2, 0.5f * scr.y), response1))
                {
                    //move forward in our dialouge array
                    currentLineIndex++;
                    if (approvalRate < 1)
                    {
                        approvalRate++;
                        //myQuest.OpenQuestWindow();
                    }
                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(12 * scr.x, 8.5f * scr.y, scr.x * 2, 0.5f * scr.y), response2))
                {
                    //skip to end of dlg;
                    currentLineIndex = dialogueText.Length - 1;
                    if (approvalRate > -1)
                    {
                        approvalRate--;
                    }
                }
            }
            //else we are at the end
            else
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                {
                    //close the dialogue box
                    showDialogue = false;
                    //set index back to 0 
                    currentLineIndex = 0;
                    //get the component movement on the character and turn that back on
                    //playerMouseLook.enabled = true;

                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible       
                    Cursor.visible = false;
                }
            }

            if (GUI.Button(new Rect(0.25f * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Shop"))
            {
               /* myShop.showShopInv = true;
                LinearInventory.showInv = true;
                LinearInventory.currentShop = myShop;
                showDlg = false;*/
            }
        }
    }


}
