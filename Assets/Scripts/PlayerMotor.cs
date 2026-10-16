using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 14f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    public bool canJump = true;
    public bool isClimbing = false;
    public float climbSpeed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        if (isClimbing)
        {
            // Движение вверх и вниз по лестнице при нажатии W и S
            moveDirection.y = input.y * climbSpeed;
            controller.Move(moveDirection * Time.deltaTime);
            return;
        }

        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void ExitClimbMode()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f / 2 + 0.1f) && isClimbing)
        {
            isClimbing = false;
        }
    }
}
