using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float speed = 5.0f; // The player's movement speed
    public float jumpHeight = 2.0f; // The height of the player's jump
    public float gravity = -9.81f; // The strength of gravity
    public float sensitivity = 100.0f; // The camera's sensitivity to mouse movement
    public float smoothTime = 0.1f; // The time it takes for the camera to smooth out its movement

    private CharacterController controller; // The Character Controller component attached to the player
    private Vector3 velocity; // The player's current velocity
    private float yaw; // The camera's yaw (left and right rotation)
    private float pitch; // The camera's pitch (up and down rotation)

    void Start()
    {
        // Get the Character Controller component attached to the player
        controller = GetComponent<CharacterController>();

        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if the player is pressing the "Jump" button
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // The player is pressing the "Jump" button and is on the ground, so make them jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Get the mouse movement deltas
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Update the camera's yaw and pitch based on the mouse movement deltas
        yaw += mouseX * sensitivity * Time.deltaTime;
        pitch -= mouseY * sensitivity * Time.deltaTime;

        // Clamp the camera's pitch to prevent the player from seeing behind themselves
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

        // Calculate the camera's desired rotation
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0.0f);

        // Smoothly rotate the camera towards the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);

        // Check if the player is pressing the "Forward" or "Backward" buttons
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the player's movement vector based on the camera's rotation
        Vector3 move = transform.right * x + transform.forward * z;

        // Normalize the movement
        move = move.normalized;

        // Update the player's velocity
        velocity.x = move.x * speed;
        velocity.z = move.z * speed;

        // Apply gravity to the player's velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the player using the Character Controller component
        controller.Move(velocity * Time.deltaTime);
    }
}