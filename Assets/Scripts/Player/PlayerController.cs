using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code relating to player
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;

    //mouse input
    ////rotate the camera up and down
    ////rotate the character left and right
    //keyboard input
    ////move the character
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    Camera camera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
       // Cursor.lockState = CursorLockMode.Locked;
    }
    private void onGUI()
    {
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

/*
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





