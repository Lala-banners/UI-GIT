using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setting up druid, barbarian etc abilities and player class
[System.Serializable]
public class PlayerProfession
{

    public string ProfessionName = "Profession";
    public PlayerStats playerStats;
    public PlayerController player;

    public string AbilityName = "Ability Name";
    public string AbilityDescription = "Does an action";

    public BaseStats[] defaultStats;

}
