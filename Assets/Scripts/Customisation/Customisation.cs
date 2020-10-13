using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class Customisation : MonoBehaviour
{
    public static Customisation instance = null;

    #region VARIABLES
    public int currentHeight;
    [SerializeField]
    private string TextureLocation = "Character/";
    public string LoadScene = "GameScene";
    public enum CustomiseParts { Skin, Hair, Mouth, Eyes, Clothes, Armour };

    //[Enum.GetNames(typeof(CustomiseParts)).Length]; this part gives us the number of customiseable parts = 6
    //an array of List<texture2d>
    //= 6 Lists
    public List<Texture2D>[] partsTexture = new List<Texture2D>[Enum.GetNames(typeof(CustomiseParts)).Length];
    private int[] currentPartsTextureIndex = new int [Enum.GetNames(typeof(CustomiseParts)).Length];
    #endregion

    #region FUNCTIONS
    public void SceneChanger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    //Renderer for character mesh so we can reference material list within script for changing visuals
    public Renderer characterRenderer;

    //First number - which body part
    //Second number - which version of body part
    //partsTexture[0][0] = Skin_0
    //partsTexture[0][1] = Skin_1
    //partsTexture[0][2] = Skin_2
    //partsTexture[0][3] = Skin_3
    //partsTexture[1][1] = Eyes_0
    //partsTexture[2][0] = Hair_1

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
                tempTexture = (Texture2D) Resources.Load(TextureLocation + part + "_" + textureCount);
                
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
        // Determine if we are actually customising
            
            // if not return;

        GUI.Box(new Rect(10, 10, 110, 210), "Visuals");

        currentHeight = 40;

        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };

        for (int i = 0; i < names.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
            {
                //Setting direction to right
                SetTexture(names[i], -1);
            }

            GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names[i]);

            if (GUI.Button(new Rect(80, currentHeight + i * 30, 20, 20), ">"))
            {
                //Setting direction to left
                SetTexture(names[i], 1);
            }
        }
    }
    #endregion

}
