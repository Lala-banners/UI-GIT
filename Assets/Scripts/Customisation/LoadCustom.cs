using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCustom : MonoBehaviour
{
    public Player player;
    public Renderer characterMesh;

    // Start is called before the first frame update
    void Start()
    {
        //Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Load()
    {

        player.name = PlayerPrefs.GetString("CharacterName");
        player.gameObject.name = player.name;
        //SceneManager.LoadScene(1);

        SetTexture("Skin", PlayerPrefs.GetInt("currentPartsTextureIndex[0]"));
        SetTexture("Hair", PlayerPrefs.GetInt("currentPartsTextureIndex[1]"));
        SetTexture("Mouth", PlayerPrefs.GetInt("currentPartsTextureIndex[2]"));
        SetTexture("Eyes", PlayerPrefs.GetInt("currentPartsTextureIndex[3]"));
        SetTexture("Clothes", PlayerPrefs.GetInt("currentPartsTextureIndex[4]"));
        SetTexture("Armour", PlayerPrefs.GetInt("currentPartsTextureIndex[5]"));
    }

    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int matIndex = 0;
        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/" + index) as Texture2D;
                matIndex = 0;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 4;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 5;
                break;
        }
        Material[] mats = characterMesh.materials;
        mats[matIndex].mainTexture = texture;
        characterMesh.materials = mats;
    }
}
