using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // reference to the character GameObject
    public GameObject character;

    // offset from the character's position to the camera's position
    public Vector3 offset;

    // smooth factor for camera movement
    public float smoothFactor = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // calculate the target position for the camera
        Vector3 targetPosition = character.transform.position + offset;

        // smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor);
    }
}
