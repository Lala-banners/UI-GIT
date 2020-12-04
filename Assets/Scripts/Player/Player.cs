﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// Code relating to the player
/// </summary>
[RequireComponent(typeof(CharacterController))]
//[System.Serializable]
public class Player : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// To save the player customisation to load into game scene (2).
    /// </summary>
    public int[] customisationTextureIndex;
    #endregion

    [Header("Other")]
    public float speed = 12f;
    public float gravity = -9.81f;
    
    [Header("Stamina Regen/Degen Stats")]
    [Tooltip("Amount of Stamina that will be taken")]
    [SerializeField] public float staminaDegen = 10f;

    [Tooltip("Amount of time stamina regeneration is impossible")]
    [SerializeField] public float disableStaminaRegenTime = 10f;

    [Tooltip("After there is no more stamina, wait until regen time starts")]
    [SerializeField] public float staminaRegenCooldown = 1f;

    [Header("Mana Regen/Degen Stats")]
    [SerializeField] public float disableManaRegenTime;
    [SerializeField] public float manaRegenCooldown = 5f;
    [SerializeField] public float manaDegen = 15f;

    [Header("Mana Bar")]
    public Image manaFill;
    public Slider manaSlider;
    public Gradient manaGradient;

    [Header("Stamina Bar")]
    public Image staminaFill; // Image to apply the colors to.
    public Slider staminaSlider;
    public Gradient staminaGradient;

    #region References to other scripts
    public PlayerStats.Stats stats; //Stats class inside PlayerStats
    public PlayerStats playerStats; //PlayerStats reference
    [SerializeField] public ProfessionInfo profession;
    public BaseStats[] defaultStat;
    public ProfessionInfo Profession
    {
        get { return profession; }
        set { ChangeProfession(value); }
    }
    public GameObject deathMenu;

    public Inventory inventory;
    public GameObject inventoryObject;
    #endregion

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward); //Creates line from player to infinity 
            RaycastHit hitInfo; //Get back info about what we hit
            //ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            //THIS IS HOW TO MAKE A MASK
            //Method of finding layer with name
            //Get the layer id
            int layerMask = LayerMask.NameToLayer("Interactable");

            //Moving binary 1 over to the left, method 1 if dont want to search for the name of layer
            //Actually turning it into a layer and not a value
            layerMask = 1 << layerMask;

            //If Ray hits something
            if (Physics.Raycast(ray, out hitInfo, 10f, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<NPC>(out NPC npc))
                {
                    npc.Interact(); //Override NPC because otherwise would run wrong method
                }
            }
        }
    }

    public void SetStamina()
    {
        staminaSlider.maxValue = stats.maxStamina;
        staminaSlider.value = stats.currentStamina;
        staminaFill.color = staminaGradient.Evaluate(staminaSlider.normalizedValue);
    }

    public void Awake()
    {
        //If not Customisation scene then load player data
        if (SceneManager.GetActiveScene().name != "Customisation")
        {
            //load player data
            PlayerData loadedPlayer = PlayerBinarySave.LoadPlayerData();
            if (loadedPlayer != null)
            {
                stats = loadedPlayer.playerStats;
                profession = loadedPlayer.profession;
                customisationTextureIndex = loadedPlayer.customisationTextureIndex;
            }
        }
    }

    // OnControllerColliderHit is called when the controller hits a collider while performing a Move
    private void OnControllerColliderHit(ControllerColliderHit hit) //To pick up item
    {
        InWorldItems items = hit.collider.GetComponent<InWorldItems>(); //if player collides with item
        if(items != null) //if item exists and is food inventory.item.Type == ItemType.Food
        {
            inventory.AddItem(inventory.item); //add to inventory
            print("Item has been picked up");
            items.gameObject.SetActive(false);
            //Destroy(items.gameObject);
        }
    }

    private void Update()
    {
        Interact();

        #region Health
        if (Input.GetKeyDown(KeyCode.H)) //HEALTH DECREASE
        {
            playerStats.DealDamage(10f);
            playerStats.healthHearts.UpdateHearts(playerStats.CurrentHealth, playerStats.healthHearts.maximumHealth);
            Debug.Log("Player is losing health");

            if (playerStats.CurrentHealth == 0) 
            {
                deathMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                print("You are dead");
            }
        }
        #endregion

        #region Mana
        UseMana(25f); //MANA DECREASE WHEN PRESS M
        //Mana Regen
        if (Time.time > disableManaRegenTime + manaRegenCooldown)
        {
            if (stats.currentMana < stats.maxMana)
            {
                SetMana();
                stats.currentMana += manaDegen * Time.deltaTime;
                print("Regenerating Mana");
            }
            else
            {
                stats.currentMana = stats.maxMana;
            }
        }
        #endregion

        #region Stamina
        //Stamina Regen
        if (Time.time > disableStaminaRegenTime + staminaRegenCooldown)
        {
            //If current stamina is less than max stamina (100)
            if (stats.currentStamina < stats.maxStamina)
            {
                SetStamina();
                stats.currentStamina += staminaDegen * Time.deltaTime;
            }
            else
            {
                stats.currentStamina = stats.maxStamina;
            }
        }
        #endregion
    }

    //Set Mana Slider
    public void SetMana()
    {
        manaSlider.maxValue = stats.maxMana;
        manaSlider.value = stats.currentMana;
        manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
    }

    public void UseMana(float spentMana) //Function to call Mana degen and regen
    {
        //If current mana is greater than/equal to 0 and press M
        if (stats.currentMana >= 0 && (Input.GetKey(KeyCode.M)))
        {
            //Call Mana Slider
            SetMana();
            disableManaRegenTime = Time.time;
            stats.currentMana -= manaDegen * Time.deltaTime;

            print("Spent Mana");
        }
    }

    /*private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void Move()
    {
        //x = -1 to 1
        float x = Input.GetAxis("Horizontal");
        //z = -1 to 1
        float z = Input.GetAxis("Vertical");
        //we want to move in this direction
        Vector3 move = (transform.right * x) + (transform.forward * z);
        velocity.y += gravity * Time.deltaTime;
        controller.Move((velocity + move) * speed * Time.deltaTime);
    }*/

    private void OnGUI()
    {
        //if (GUI.Button(new Rect(130, 10, 100, 20), "Level Up"))
        //{
          //  LevelUp();
        //}

        //if (GUI.Button(new Rect(130, 40, 100, 20), "Do Damage")) 
        //{
            //playerStats.DealDamage(25f);
        //}
    }

    public void LevelUp()
    {
        stats.baseStatPoints += 3;

        for (int i = 0; i < stats.baseStats.Length; i++)
        {
            stats.baseStats[i].additionalStat += 1;
        }
    }

    public void ChangeProfession(ProfessionInfo cProfession)
    {
        profession = cProfession;
        SetUpProfession();
    }

    public void SetUpProfession()
    {
        for (int i = 0; i < stats.baseStats.Length; i++)
        {
            if (profession.defaultStats.Length < i) //check if i exists in profession
            {
                stats.baseStats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }
        }
    }
}
