using UnityEngine;

[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat; //stat from class
    public int levelUpStat;
    public int additionalStat; //addition stat 
    public int finalStat
    {
        get
        {
            return defaultStat + additionalStat;
        }
    }
}

[System.Serializable]
public class PlayerStats
{
    [System.Serializable]
    public class Stats
    {
        [Header("Player Movement")]
        [SerializeField] public float speed = 6f;
        [SerializeField] public float crouchSpeed = 3;
        [SerializeField] public float sprintSpeed = 12;
        [SerializeField] public float movementSpeed;
        [SerializeField] public float jumpHeight = 1.0f;

        [Header("Current Stats")]
        [SerializeField] public int level;
        [SerializeField] public float currentMana = 100;
        [SerializeField] public float maxMana = 100;
        [SerializeField] public float currentStamina = 100;
        [SerializeField] public float maxStamina = 100;
        [SerializeField] public float disableStaminaRegen;
        [SerializeField] public float regenStamina = 30f;
        [SerializeField] public float regenHealth;

        [Header("Base Stats")]
        public BaseStats[] baseStats;
        public int baseStatPoints = 10;
    }
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    Stats stats;
    //If able to change stats then return true
    public bool SetStats(int statIndex, int amount)
    {
        stats.baseStats[statIndex].additionalStat += amount;

        //increasing
        if (amount > 0 && stats.baseStatPoints - amount < 0) //we cant add points if there are none left
        {
            return false;
        }
        else if (amount < 0 && stats.baseStats[statIndex].additionalStat + amount < 0) //additionalStat must be 0 or posititve int
        {
            return false;
        }

        //change the stats
        stats.baseStats[statIndex].additionalStat += amount;
        stats.baseStatPoints -= amount;

        return true;
    }

    //getter setter
    //Can update the Quarter Hearts with get and set 
    public float _currentHealth = 100; //_ represents field
    public float CurrentHealth //property
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);

            //everytime variable changes give it the value
            if (healthHearts != null)
            {
                healthHearts.UpdateHearts(value, maxHealth);
            }
        }
    }
    public QuarterHearts healthHearts;

    //Function for dealing damage to the player
    public void DealDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    //Function for healing
    public void Heal(float health)
    {
        CurrentHealth += health;
    }

}



