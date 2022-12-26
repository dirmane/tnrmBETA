using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float jumpHeight = 2.0f; // The height of the player's jump
    public float gravity = -9.81f; // The strength of gravity

    private CharacterController controller; // The Character Controller component attached to the player
    private Vector3 velocity; // The player's current velocity
    private bool canDoubleJump; // Whether or not the player can double jump

    void Start()
    {
        // Get the Character Controller component attached to the player
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is pressing the "Jump" button
        if (Input.GetButtonDown("Jump"))
        {
            // The player is pressing the "Jump" button
            if (controller.isGrounded)
            {
                // The player is on the ground, so make them jump
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canDoubleJump = true; // The player can now double jump
            }
            else if (canDoubleJump)
            {
                // The player is not on the ground and can double jump, so make them double jump
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canDoubleJump = false; // The player can no longer double jump
            }
        }

        // Apply gravity to the player's velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the player using the Character Controller component
        controller.Move(velocity * Time.deltaTime);
    }
}
