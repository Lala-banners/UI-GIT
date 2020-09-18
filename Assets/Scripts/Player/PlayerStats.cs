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
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float currentMana = 100;
    [SerializeField] public float maxMana = 100;
    [SerializeField] public float currentStamina = 100;
    [SerializeField] public float maxStamina = 100;
    [SerializeField] public int level;


}
