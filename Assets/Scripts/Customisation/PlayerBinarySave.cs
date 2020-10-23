using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; //Always have this if want to access Binary Formatter lib

public class PlayerBinarySave : MonoBehaviour //Save and Load to binary file!
{
    public static void SavePlayerData(Player player)
    {
        //Ref to binary formatter
        BinaryFormatter formatter = new BinaryFormatter();

        //location to save
        string path = Application.dataPath + "/doggo.dragonborn";

        //create file at file path
        FileStream stream = new FileStream(path, FileMode.Create);

        //convert to a class that can be saved
        PlayerData data = new PlayerData(player);

        //Serealize player and save it to the file
        formatter.Serialize(stream, data); //Save to file

        //Close the file
        stream.Close();
    }   
    
    public static PlayerData LoadPlayerData()
    {
        //location to save
        string path = Application.dataPath + "/doggo.dragonborn";

        //If there is a file at that path
        if (File.Exists(path))
        {
            //get the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            //read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);

            //deserialize back to a usable variable
            PlayerData data = (PlayerData)formatter.Deserialize(stream); //as Player

            //close the file
            stream.Close();

            return data;
        }
        return null;
    }
}
