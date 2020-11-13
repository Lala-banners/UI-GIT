using UnityEngine;
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
    //public CharacterController controller;
    //private Vector3 velocity;
    //public float mouseSensitivity = 100f;
    //private float xRotation = 0f;
    //Camera camera;
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
    #endregion

    public void SetStamina()
    {
        staminaSlider.maxValue = stats.maxStamina;
        staminaSlider.value = stats.currentStamina;
        staminaFill.color = staminaGradient.Evaluate(staminaSlider.normalizedValue);
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision playerCol)
    {
        //If player collides with object tagged as Enemy
        if (playerCol.gameObject.Equals("Player"))
        {
            print("Player is hit");
            //Player gets dealt damage
            playerStats.DealDamage(10f);
            playerStats.healthHearts.UpdateHearts(playerStats.CurrentHealth, playerStats.healthHearts.maximumHealth); //Updates the heart sprites
            //Debug.Log("Player is losing health");
        }
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

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        //camera = Camera.main;
    }


    private void Update()
    {
        Interact();

        if (Input.GetKeyDown(KeyCode.H)) //TEST FOR HEALTH DECREASE IT WORKS
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
        
        //MouseLook();
        //Move();

        UseMana(25f); //spend mana when press M

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
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Keybinding code here
        {
            Ray ray; //Creates line from middle of the screen to infinity 
            RaycastHit hitInfo; //Get back info about what we hit
            ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

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
                if(hitInfo.collider.TryGetComponent<NPC>(out NPC npc))
                {
                    npc.Interact(); //Override NPC because otherwise would run wrong method
                }
            }

            //Pick up items here from inventory 
            //If raycast hit an item from the ground, then pick up item off the ground. Create new class for ItemOnGround()
            //In Item on ground class make reference to item
        }
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
