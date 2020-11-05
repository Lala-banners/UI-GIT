using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


/// <summary>
/// Code relating to the player
/// </summary>
[RequireComponent(typeof(CharacterController))]
//[System.Serializable]
public class Player : MonoBehaviour
{
    #region Variables
    #region Health
    [Header("Health")]
    private bool disableRegen = false;
    private float disableRegenTime;
    public float RegenCooldown = 5f;
    #endregion



    #region Stamina
    [Header("Stamina")]
    public Image staminaFill; // Image to apply the colors to.
    public Slider staminaSlider;
    public Gradient staminaG;
    public float disableStaminaRegenTime;
    public float staminaRegenCooldown = 1f;
    public float StaminaDegen = 30f;
    #endregion

 
    #region Mana
    [Header("Mana")]
    public Image manaFill;
    public Slider manaSlider;
    public Gradient manaG;
    public float disableManaRegen;
    public float manaRegenCooldown = 5f;
    public float manaDegen = 30f;
    #endregion

    [Space]

    /// <summary>
    /// To save the player customisation to load into game scene (2).
    /// </summary>
    public int[] customisationTextureIndex;
    #endregion

    [Header("Other")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    Camera camera;

    #region References to other scripts
    public PlayerStats.Stats stat; //Stats class inside PlayerStats
    public PlayerStats playerStats; //PlayerStats reference
    [SerializeField] public PlayerProfession profession;
    public BaseStats[] defaultStat;
    public PlayerProfession Profession
    {
        get { return profession; }
        set { ChangeProfession(value); }
    }
    #endregion

    #region FUNctions
    public void Awake()
    {
        //If not Customisation scene then load player data
        if (SceneManager.GetActiveScene().name != "Customisation")
        {
            //load player data
            PlayerData loadedPlayer = PlayerBinarySave.LoadPlayerData();
            if (loadedPlayer != null)
            {
                stat = loadedPlayer.playerStats;
                profession = loadedPlayer.profession;
                customisationTextureIndex = loadedPlayer.customisationTextureIndex;
            }
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
    }


    private void MouseLook()
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
    }
    private void Update()
    {

        MouseLook();
        Move();
        
        #region Health Regen
        if (!disableRegen)
        {
            playerStats.CurrentHealth += stat.regenHealth * Time.deltaTime;
        }
        else
        {
            if (Time.time > disableRegenTime + staminaRegenCooldown) //after 5 seconds start regeneration 
            {
                disableRegen = false;
            }
        }
        #endregion

        #region Stamina Regen
        //display current stamina
        if (Time.time > disableStaminaRegenTime + staminaRegenCooldown)
        {
            if (stat.currentStamina < stat.maxStamina)
            {
                stat.currentStamina += stat.regenStamina * Time.deltaTime;
                SetStamina();
            }
            else
            {
                stat.currentStamina = stat.maxStamina;
            }
        }
        #endregion

        #region Mana Regen
        //display current mana
        if (!disableRegen)
        {
            stat.currentMana += stat.regenMana * Time.deltaTime;
            SetMana();
        }
        else
        {
            if (Time.time > disableRegenTime + manaRegenCooldown) //after 5 seconds start regeneration 
            {
                disableRegen = false;
            }
        }
        #endregion
    }

    #region Display Mana Bar
    public void SetMana()
    {
        manaSlider.maxValue = stat.maxMana;
        manaSlider.value = stat.currentMana;
        manaFill.color = manaG.Evaluate(manaSlider.normalizedValue);
    }
    #endregion

    public void UseMana()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMana();
        }
        else
        {
            print("Did not use mana");
        }
    }

    #region Display Stamina
    public void SetStamina()
    {
        staminaSlider.maxValue = stat.maxStamina;
        staminaSlider.value = stat.currentStamina;
        staminaFill.color = staminaG.Evaluate(staminaSlider.normalizedValue);
        
    }

    #endregion
    public void LevelUp()
    {
        stat.baseStatPoints += 3;

        for (int i = 0; i < stat.baseStats.Length; i++)
        {
            stat.baseStats[i].additionalStat += 1;
        }
    }

    //Function for dealing damage to the player
    public void DealDamage(float damage)
    {
        playerStats.CurrentHealth -= damage;
        disableRegen = true;
        disableRegenTime = Time.time;
    }

    //Function for healing
    public void Heal(float health)
    {
        playerStats.CurrentHealth += health;
    }
    


    #region Customisation
    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfession();
    }

    public void SetUpProfession()
    {
        for (int i = 0; i < stat.baseStats.Length; i++)
        {
            if (profession.defaultStats.Length < i) //check if i exists in profession
            {
                stat.baseStats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }
        }
    }
    #endregion

}


/*private void OnGUI()
{
    if(GUI.Button(new Rect(130, 10, 100, 20), "Level Up"))
    {
        LevelUp();
    }

    if (GUI.Button(new Rect(130, 40, 100, 20), "Do Damage"))
    {
        DealDamage(25f);
    }
}

#endregion
}*/

/* Commented out Save() and Load()
 //Functions Save and Load 

    player stats
    public int level = 1;
    public float health = 55;

    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }


    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;
     
        Vector3 pos = new Vector3(data.position[0],
                data.position[1],
                data.position[2]);

        transform.position = pos;
    }
*/
#endregion