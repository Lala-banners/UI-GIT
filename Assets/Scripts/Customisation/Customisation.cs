using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[System.Serializable]

public class Customisation : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    public static Customisation instance = null;
    //the defaults for each profession
    [SerializeField] PlayerProfession[] playerProfessions;

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
    private int[] currentPartsTextureIndex = new int [Enum.GetNames(typeof(CustomiseParts)).Length];

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

    }
    public void SceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// James help to make warrior babe in game environment
    /// </summary>
    private void Awake()
    {
        if(instance == null)
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
    void SetTexture(string type, int direction)
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
        currentTexture += direction; //Move into direction

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

    /// <summary>
    /// Apply the selected customization stats to the passed renderer
    /// </summary>
    /// <param name="renderer">the renderer being customised</param>
    public void ApplyCustomisation(Renderer renderer)
    {
        // do the customisation like in SetTextures
        
    }

    //Updates specifically when GUI elements are called
    private void OnGUI()
    {
        StatsOnGUI();
        CustomiseOnGUI();
        ProfessionsOnGUI();
    }

    public void ProfessionsOnGUI()
    {
        float currentHeight = 0;

        GUI.Box(new Rect(Screen.width - 170, 230, 155, 60), "Profession");

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

        GUI.Box(new Rect(Screen.width - 170, Screen.height - 90, 155, 80), "Display");
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 100 + 30, 100, 20), player.Profession.ProfessionName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 100 + 30, 100, 20), player.Profession.AbilityName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 100 + 30, 100, 20), player.Profession.AbilityDescription);
    }

    //IMGUI prototype - comment out onGUI methods and replace with real GUI elements
    private void StatsOnGUI()
    {
        float currentHeight = 40;
        GUI.Box(new Rect(Screen.width - 140, 10, 130, 210), "Stats");

        for (int i = 0; i < player.playerStats.baseStats.Length; i++)
        {
            BaseStats stat = player.playerStats.baseStats[i];

            if (GUI.Button(new Rect(Screen.width - 135, currentHeight + i * 30, 20, 20), "-"))
            {
                player.playerStats.SetStats(i, -1);
            }

            GUI.Label(new Rect(Screen.width - 110, currentHeight + i * 30, 70, 20),
                stat.baseStatName + ": " + stat.finalStat);

            if (GUI.Button(new Rect(Screen.width - 40, currentHeight + i * 30, 20, 20), "+"))
            {
                player.playerStats.SetStats(i, -1);
            }
            i++;
        }        
    }

    
    private void CustomiseOnGUI()
    {
        // Determine if we are actually customising

        // if not return;

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
        
    }
    #endregion

}