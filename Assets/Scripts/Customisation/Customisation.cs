using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[System.Serializable]

public class Customisation : MonoBehaviour
{
    [SerializeField] public Player player;
    PlayerStats playerStats;
    public static Customisation instance = null;
    [SerializeField] PlayerProfession[] playerProfessions; //the defaults for each profession


    #region VARIABLES
    public int currentHeight;
    [SerializeField]
    private string TextureLocation = "Character/";
    public string LoadScene = "GameScene";
    public enum CustomiseParts { Skin, Hair, Mouth, Eyes, Clothes, Armour };
    public Vector2 scrollPosition = Vector2.zero;

    //[Enum.GetNames(typeof(CustomiseParts)).Length]; this part gives us the number of customiseable parts = 6
    //an array of List<texture2d>
    //= 6 Lists
    public List<Texture2D>[] partsTexture = new List<Texture2D>[Enum.GetNames(typeof(CustomiseParts)).Length];
    [SerializeField] private int[] currentPartsTextureIndex = new int[Enum.GetNames(typeof(CustomiseParts)).Length];

    //Renderer for character mesh so we can reference material list within script for changing visuals
    public Renderer characterRenderer;



    #endregion

    #region FUNCTIONS
    private void Start()
    {
        int partCount = 0;
        foreach (string part in Enum.GetNames(typeof(CustomiseParts)))
        {

            Texture2D tempTexture;
            int textureCount = 0;

            partsTexture[partCount] = new List<Texture2D>();

            do
            {
                //Load Textures so they can be set
                tempTexture = (Texture2D)Resources.Load(TextureLocation + part + "_" + textureCount);

                //If temp texture exists
                if (tempTexture != null)
                {
                    //Add this texture to a collection
                    partsTexture[partCount].Add(tempTexture);
                }
                textureCount++;
            } while (tempTexture != null);
            partCount++;
        }

        if (player == null)
        {
            Debug.LogError("player in Customisation is null");
        }
        else
        {
            if (player.customisationTextureIndex.Length != 0)
            {
                currentPartsTextureIndex = player.customisationTextureIndex;
            }
        }

        if (playerProfessions == null
            && playerProfessions.Length > 0)
        {
            player.Profession = playerProfessions[0];
        }

        //string[] of each body part =  Enum.GetNames(typeof(CustomiseParts))
        //["Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour"]
        foreach (string part in Enum.GetNames(typeof(CustomiseParts))) //Loop through array of body parts
        {
            SetTexture(part, true);
        }
    }

    private void Update()
    {
        //OnClickRandomize();
        //ChangeSkinColour(true);
    }

    public void SceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //Function to set a texture 
    /// <summary>
    /// Sets the texture based on the type we ask for, and the direction we are moving
    /// </summary>
    /// <param name="type"></param>
    /// <param name="moveRight">If set to true, use the next texture for this item, if false, the previous</param>
    void SetTexture(string type, bool moveRight)
    {
        int partIndex = 0;

        switch (type)
        {
            case "Skin":
                partIndex = 0;
                break;

            case "Hair":
                partIndex = 1;
                break;

            case "Mouth":
                partIndex = 2;
                break;

            case "Eyes":
                partIndex = 3;
                break;

            case "Clothes":
                partIndex = 4;
                break;

            case "Armour":
                partIndex = 5;
                break;

            default:
                Debug.LogError("Invalid set texture type");
                break;
        }

        int max = partsTexture[partIndex].Count;

        //Getting current texture, increase number by 1 and storing that number (moving to next texture)
        int currentTexture = currentPartsTextureIndex[partIndex];
        if(moveRight)
        {
            // move to the right
            currentTexture++;
        }
        else
        {
            // move to the left
            currentTexture--;
        }

        if (currentTexture < 0)
        {
            currentTexture = max - 1;
        }
        else if (currentTexture > max - 1)
        {
            currentTexture = 0;
        }
        currentPartsTextureIndex[partIndex] = currentTexture;

        //These are the materials that the texture will be changed
        Material[] mats = characterRenderer.materials;
        mats[partIndex].mainTexture = partsTexture[partIndex][currentTexture];
        characterRenderer.materials = mats;
    }

    public void SaveCharacter()
    {
        player.customisationTextureIndex = currentPartsTextureIndex; //storing array of index in Player and saving it
        PlayerBinarySave.SavePlayerData(player); //When save button is pressed will save
    }

    public void SaveAndPlay() //For save and play button
    {
        SaveCharacter();
        SceneManager.LoadScene(1); //load game scene
    }

    /*public void CustomiseSkin() //for customising skin
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        int i = 0;
        foreach (CustomiseParts Skin in Enum.GetValues(typeof(CustomiseParts)))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Setting direction to right
                SetTexture(names.ToString(), false);
            }

            //GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names.ToString());

            // if (GUI.Button(new Rect(90, currentHeight + i * 30, 20, 20), ">"))
            //{
            //Setting direction to left
            //  SetTexture(names.ToString(), 1);
            //}

            i++;
        }
    }*/


    public void ChangeSkinColour(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[0].ToString(), increase);
        SaveCharacter();
    }

    public void ChangeHairColour(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[1].ToString(), increase);
        SaveCharacter();
    }

    public void ChangeMouth(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[2].ToString(), increase);
        SaveCharacter();
    }

    public void ChangeEyeColour(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[3].ToString(), increase);
        SaveCharacter();
    }

    public void ChangeClothesColour(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[4].ToString(), increase);
        SaveCharacter();
    }

    public void ChangeArmourColour(bool increase)
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        SetTexture(names[5].ToString(), increase);
        SaveCharacter();
    }

    public void OnClickRandomize()
    {
        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
        //SetTexture(names[0], UnityEngine.Random.Range(-5, 5));
        for(int i = 0; i < names.Length; i++)
        {
            // Get this parts material
            Material mat = characterRenderer.materials[i];

            // Get this parts textures
            List<Texture2D> partTextures = partsTexture[i];

            mat.mainTexture = partTextures[UnityEngine.Random.Range(0, partTextures.Count)];
        }
    }

    public void ChooseProfession()
    {
        //player.Profession = profession;
        //player.Profession.AbilityDescription;
        //player.Profession.AbilityName;
    }

    //public void SetBaseStats() {}
  
#region Commented out OnGUI
/*
PlayerPrefs.SetInt("Skin Index", currentPartsTextureIndex[0]);
PlayerPrefs.SetInt("Hair Index", currentPartsTextureIndex[1]);
PlayerPrefs.SetInt("Eyes Index", currentPartsTextureIndex[2]);
PlayerPrefs.SetInt("Mouth Index", currentPartsTextureIndex[3]);
PlayerPrefs.SetInt("Clothes Index", currentPartsTextureIndex[4]);
PlayerPrefs.SetInt("Armour Index", currentPartsTextureIndex[5]);

//Set Character name in own time
//PlayerPrefs.SetString("Character Name", characterName);

for (int i = 0; i< player.playerStats.baseStats.Length; i++)
{
    PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + "default stat", player.playerStats.baseStats[0].defaultStat);
    PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + "additional stat", player.playerStats.baseStats[0].additionalStat);
    PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + "levelUpStat", player.playerStats.baseStats[0].levelUpStat);
}
PlayerPrefs.SetString("Character Profession", player.Profession.ProfessionName); 
#endregion
}


//Updates specifically when GUI elements are called
private void OnGUI()
{
StatsOnGUI();
CustomiseOnGUI();
ProfessionsOnGUI();

if (GUI.Button(new Rect(10,250,120,20), "Save and Play"))
{
    SaveCharacter();
    SceneManager.LoadScene(1); //load game scene
}
}

public void ProfessionsOnGUI()
{
float currentHeight = 0;

GUI.Box(new Rect(Screen.width - 170, 230, 155, 100), "Profession");

scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 170, 250, 155, 50), scrollPosition, 
                                     new Rect(0,0,100,30 * playerProfessions.Length));

int i = 0;
foreach (PlayerProfession profession in playerProfessions)
{
    if (GUI.Button(new Rect(20, currentHeight + i * 30, 100, 20), profession.ProfessionName))
    {
        player.Profession = profession;
    }
    i++;
}

GUI.EndScrollView();

GUI.Box(new Rect(Screen.width - 170, Screen.height - 150, 155, 150), "Class Display");
GUI.Label(new Rect(Screen.width - 140, Screen.height - 150 + 30, 100, 20), player.Profession.ProfessionName);
GUI.Label(new Rect(Screen.width - 140, Screen.height - 150 + 50, 100, 20), player.Profession.AbilityName);
GUI.Label(new Rect(Screen.width - 140, Screen.height - 150 + 70, 100, 100), player.Profession.AbilityDescription);
}

//IMGUI prototype - comment out onGUI methods and replace with real GUI elements
private void StatsOnGUI()
{
float currentHeight = 40;
                               //X  //Y  //W  //H
GUI.Box(new Rect(Screen.width - 170, 10, 155, 210), "Stats");

for (int i = 0; i < player.stat.baseStats.Length; i++)
{
    BaseStats stat = player.stat.baseStats[i];

    if (GUI.Button(new Rect(Screen.width - 165, currentHeight + i * 30, 20, 20), "-"))
    {
        playerStats.SetStats(i, -1);
    }

    GUI.Label(new Rect(Screen.width - 140, currentHeight + i * 30, 100, 20),
        stat.baseStatName + ": " + stat.finalStat);

    if (GUI.Button(new Rect(Screen.width - 40, currentHeight + i * 30, 20, 20), "+"))
    {
        playerStats.SetStats(i, 1);
    }
    //i++;
}        
}


private void CustomiseOnGUI()
{
float currentHeight = 40;
GUI.Box(new Rect(10, 10, 110, 210), "Visuals");
//string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
int i = 0;
foreach (CustomiseParts names in Enum.GetValues(typeof(CustomiseParts)))
{
    if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
    {
        //Setting direction to right
        SetTexture(names.ToString(), -1);
    }

    GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names.ToString());

    if (GUI.Button(new Rect(90, currentHeight + i * 30, 20, 20), ">"))
    {
        //Setting direction to left
        SetTexture(names.ToString(), 1);
    }

    i++;
}

}*/
#endregion
#endregion
}