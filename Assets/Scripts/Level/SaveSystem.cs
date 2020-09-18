using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem : MonoBehaviour
{
    //How to Create a Save path to save a point in the game
    public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //"c:/windows.something/player/sav
        //string path = Application 
        //Creates the file name +path only, to be used later
        string path = Application.dataPath + "/player.sav";

        //Opens the file
        FileStream stream = new FileStream(path, FileMode.Create);

        //creates the data to be saved
        PlayerData data = new PlayerData(player);

        //writes the data to the file (also converts data to text)
        formatter.Serialize(stream, data);


        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.dataPath + "/player.sav";

        if (File.Exists(path))
        {
            //load data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //save this as PlayerData
            //as converts the return the PlayerData as an object
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }


    }
    
   

}
