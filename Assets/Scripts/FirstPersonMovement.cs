using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f; // The speed at which the player moves
    public float mouseSensitivity = 2.0f; // The sensitivity of the mouse look
    public float jumpHeight = 2.0f; // The height of the player's jump
    public float gravity = -9.81f; // The strength of gravity

    private CharacterController controller; // The Character Controller component attached to the player
    private Vector3 velocity; // The player's current velocity
    private bool canDoubleJump; // Whether or not the player can double jump

    void Start()
    {
        // Get the Character Controller component attached to the player
        controller = GetComponent<CharacterController>();

        // Lock the cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the input from the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player based on the mouse input
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera based on the mouse input
        Camera.main.transform.Rotate(Vector3.left * mouseY);

        // Get the input from the keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the player's movement vector based on the input
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        // Normalize the movement vector
        move = move.normalized;

        // Check if the player is pressing the "Jump" button
        if (controller.isGrounded)
        {
            // The player is on the ground, so make them jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = true; // The player can double jump
        }
        else if (canDoubleJump)
        {
            // The player is not on the ground and can double jump, so make them jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false; // The player can no longer double jump
        }
        // Apply gravity to the player's velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on their velocity
        controller.Move(velocity * Time.deltaTime * movementSpeed);

        // Check if the player's y-position is less than the respawn y-position
        if (transform.position.y < -50.0f)
        {
            // The player's y-position is less than the respawn y-position, so respawn them
            transform.position = new Vector3(0.0f, 3.0f, -2.0f);
        }
    }
}
