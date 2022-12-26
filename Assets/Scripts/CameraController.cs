using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacedMovement : MonoBehaviour
{
    public float speed = 5.0f; // The player's movement speed
    public Transform cameraTransform; // The transform of the camera object

    private CharacterController controller; // The Character Controller component attached to the player
    private Vector3 velocity; // The player's current velocity

    void Start()
    {
        // Get the Character Controller component attached to the player
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is pressing the "Forward" or "Backward" buttons
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the player's movement vector based on the camera's rotation
        Vector3 move = cameraTransform.right * x + cameraTransform.forward * z;

        // Normalize the movement vector
        move = move.normalized;

        // Update the player's velocity
        velocity.x = move.x * speed;
        velocity.z = move.z * speed;

        // Move the player using the Character Controller component
        controller.Move(velocity * Time.deltaTime);
    }
}
