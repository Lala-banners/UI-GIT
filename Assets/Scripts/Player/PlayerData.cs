//can be used in Unity
[System.Serializable]

public class PlayerData
{
    public PlayerStats.Stats playerStats;
    public ProfessionInfo profession;
    public int[] customisationTextureIndex;

    //This will run when we create new player data (class, name, variable will create a constructor)
    //Constructor is a method referencing the class inside the method
    public PlayerData(Player player)
    {
        playerStats = player.stats;
        profession = player.Profession;
        customisationTextureIndex = player.customisationTextureIndex;

    }

}
