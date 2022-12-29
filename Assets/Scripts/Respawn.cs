using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint; // The position where the player will respawn
    public float respawnY = -50.0f; // The y-position at which the player will respawn

    void Update()
    {
        // Check if the player's y-position is less than the respawn y-position
        if (transform.position.y <= respawnY)
        {
            // The player's y-position is less than the respawn y-position, so respawn them
            transform.position = respawnPoint.position;
        }
    }
}
