using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [Header("Player Movement")]
    [SerializeField] public float speed = 6f;
    [SerializeField] public float crouchSpeed = 3;
    [SerializeField] public float sprintSpeed = 12;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float jumpHeight = 1.0f;
                     
    [Header("Current Stats")]
    [SerializeField] public int level;
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float currentMana = 100;
    [SerializeField] public float maxMana = 100;
    [SerializeField] public float currentStamina = 100;
    [SerializeField] public float maxStamina = 100;

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
            //everytime variable changes give it the value
            if (healthHearts != null)
            {
                healthHearts.UpdateHearts(value, maxHealth);
            }
        }
    }
    public QuarterHearts healthHearts; 


}
