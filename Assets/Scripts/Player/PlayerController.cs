using UnityEngine;
[System.Serializable]
/// <summary>
/// Code relating to the player
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    Camera camera;
    private bool disableRegen = true;
    private float disableRegenTime;
    public float RegenCooldown = 5f;
    #endregion

    #region References to other scripts 
    public PlayerStats playerStats;
    public Customisation customisation;
    public PlayerProfession profession;
    public BaseStats[] defaultStat;
    public PlayerProfession Profession
    {
        get
        {
            return profession;
        }
            set
        {
            ChangeProfession(value);
        }
    }
    #endregion
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
       // Cursor.lockState = CursorLockMode.Locked;

        //Customisation.instance.ApplyCustomisation(someRenderer)
    }

    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfession();
    }

    public void SetUpProfession()
    {
        for (int i = 0; i < playerStats.baseStats.Length; i++)
        {
            if (profession.defaultStats.Length < i) //check if i exists in profession
            {
                playerStats.baseStats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }
        }
    }

    private void Update()
    {
        if (!disableRegen)
        {
            playerStats.CurrentHealth += playerStats.regenHealth * Time.deltaTime;
        }
        else
        {
            if (Time.time > disableRegenTime + RegenCooldown) //after 5 seconds start regeneration 
            {
                disableRegen = false;
            }
        }
    }

    public void LevelUp()
    {
        playerStats.baseStatPoints += 3;

        for (int i = 0; i < playerStats.baseStats.Length; i++)
        {
            playerStats.baseStats[i].additionalStat += 1;
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

    private void OnGUI()
    {
        if(GUI.Button(new Rect(130, 10, 100, 20), "Level Up"))
        {
            LevelUp();
        }

        if (GUI.Button(new Rect(130, 40, 100, 20), "Do Damage"))
        {
            DealDamage(25f);
        }

        //Display current health
        //display current mana
        //display current stamina

        //  MouseLook();
        // Move();
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
}
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





