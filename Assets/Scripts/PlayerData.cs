using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//can be used in Unity
[System.Serializable]

public class PlayerData
{
    public int level;
    public float health;
    public float[] position; // = If we want to save a position - Vector3 

    //This will run when we create new player data (class, name, variable will create a constructor)
    //Constructor is a method referencing the class inside the method
    public PlayerData(PlayerController player)
    {
        level = player.level;
        health = player.health;

        position = new float[3]; //Only have 3 numbers (Vector3)
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;


    }
}
