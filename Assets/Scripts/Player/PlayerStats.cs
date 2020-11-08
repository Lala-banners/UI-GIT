using UnityEngine;
using System;

[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat; //stat from the class
    public int levelUpStat;
    public int additionalStat; //bonus stats
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
        [SerializeField] public float crouchSpeed = 3f;
        [SerializeField] public float sprintSpeed = 12f;
        [SerializeField] public float movementSpeed;
        [SerializeField] public float jumpHeight = 1.0f;

        [Header("Mana Stats")]
        [SerializeField] public float currentMana = 100f;
        [SerializeField] public float maxMana = 100f;
        [SerializeField] public float regenMana = 20f;

        [Header("Stamina Stats")]
        [Tooltip("Amount of Stamina")]
        [SerializeField] public float currentStamina = 100f;
        [Tooltip("Maximum amount of Stamina")]
        [SerializeField] public float maxStamina = 100f;
        [Tooltip("Amount of Stamina that will be regenerated")]
        [SerializeField] public float regenStamina = 30f;

        [Header("General Stats")]
        [SerializeField] public int level;
        [SerializeField] public float defaultStat;

        [Tooltip("Regen Health")]
        [SerializeField] public float regenHealth = 5f;

        [Header("Base Stats")]
        public BaseStats[] baseStats;
        public int baseStatPoints = 13;
    }

    [Header("Health")]
    private bool disableRegen = false;
    private float disableRegenTime;
    public float RegenCooldown = 5f;

    Stats stats;
    public QuarterHearts healthHearts;

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
            _currentHealth = Mathf.Clamp(value, 0, healthHearts.maximumHealth);

            //everytime variable changes give it the value
            if (healthHearts != null)
            {
                healthHearts.UpdateHearts(healthHearts.currentHealth, healthHearts.maximumHealth);
            }
        }
    }

    //Function for dealing damage to the player
    public void DealDamage(float damage)
    {
        CurrentHealth = CurrentHealth -= damage;
        disableRegen = true;
        disableRegenTime = Time.time;
        healthHearts.UpdateHearts(healthHearts.currentHealth, healthHearts.maximumHealth); //update the hearts
    }

    //Function for healing
    public void Heal(float health)
    {
        CurrentHealth += health;
    }

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


    
}



