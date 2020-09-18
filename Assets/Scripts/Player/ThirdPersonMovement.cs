using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third person movement
/// </summary>
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    float turnSmoothVelocity;
    public Vector3 playerVelocity;

    [SerializeField] private PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        Jump();
        Movement();
    }
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                                                                        + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                                            ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float movementSpeed = playerController.playerStats.speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementSpeed = playerController.playerStats.sprintSpeed;
            }
            else if(Input.GetKey(KeyCode.C))
            {
                movementSpeed = playerController.playerStats.crouchSpeed;
            }

            controller.Move(moveDir * movementSpeed * Time.deltaTime);
        }
    }
    void Jump()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(playerController.playerStats.jumpHeight * -3.0f * gravity);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}