using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // movement speed
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // get input for vertical movement
        float verticalInput = Input.GetAxis("Vertical");

        // calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // apply movement
        transform.position += movement * speed * Time.deltaTime;
    }
}