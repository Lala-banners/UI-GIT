using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approval : MonoBehaviour
{
    #region Approval Variables
    public string[] dislikeTxt, neutralTxt, likeTxt;
    public int approval;
    public string response1, response2; //response of player (yes or no)
    #endregion

    #region Dialogue Variables
    public bool showDialogue;
    public int currentLineIndex;
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

    // OnGUI is called for rendering and handling GUI events
    private void OnGUI()
    {
        if(showDialogue)
        {
            //set up ratio for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, scr.y * 3), name + " : " + dialogueText[currentLineIndex]);

        }
    }


}
