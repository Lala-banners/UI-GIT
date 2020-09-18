using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    //Dictionary is similar to a list

    public Text up, down, left, right, jump;

    private GameObject currentKey;
    public Color32 changedKey = new Color32(39,171,249,255);//blue
    public Color32 selectedKey = new Color32(239,116,36,255);//orange
    

    private void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);

        //Can be stored as string -> string KeyCode button = keys["Left"].ToString();

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyBindScript.keys["Up"])) //make character move forward
        {
            Debug.Log("Character moves forward");
        }

        if (Input.GetKeyDown(KeyBindScript.keys["Down"])) //make character move down
        {
            Debug.Log("Character moves down");
        }

        if (Input.GetKeyDown(KeyBindScript.keys["Left"])) //make character move left
        {
            Debug.Log("Character moves left");
        }

        if (Input.GetKeyDown(KeyBindScript.keys["Right"])) //make character move right
        {
            Debug.Log("Character moves right");
        }

        if (Input.GetKeyDown(KeyBindScript.keys["Jump"])) //make character jump
        {
            Debug.Log("Character jumps");
        }
    }

     
    #region Method to set up new keys 
    private void OnGUI()
    {
        string newKey = "";
        Event e = Event.current;
        if (currentKey != null) //when currentKey is null -> want to reassign a key
        {
            if (e.isKey)
            {
                //HINT FOR PRESS ANY KEY TO CONTINUE
                Debug.Log(e.keyCode.ToString());
                newKey = e.keyCode.ToString();
            }

            //There is an issue getting SHift key, the following will fix this
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "")//if we have set a key, this is not empty
            {
                //we change our dictionary (that means our keybind changes too)
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey); //newKey string
                //change text of our button
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changedKey;
                currentKey = null;
                SaveKeys();
            }
        }
    }
    #endregion


    #region Method to change keys to whatever we want
    public void ChangeKey(GameObject clickKey)
    {
        currentKey = clickKey;
        if(clickKey != null)
        {
            currentKey.GetComponent<Image>().color = selectedKey;
        }
    }
    #endregion

    #region To Save keys when we change them
    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
    #endregion
}
