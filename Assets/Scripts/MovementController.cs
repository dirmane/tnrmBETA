using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // movement speed
    public float speed = 5.0f;

    // rotation smoothness
    public float rotationSmoothness = 10.0f;

    //animator 
    private Animator = animator;

    void Start() 
    {

        animator = GetComponent<Animator>();

    }

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

        // if there is no input, don't change the rotation
        if (movement.magnitude > 0)
        {
            animator.SetBool("isWalking", true); 
            // get the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // smooth out the rotation using Quaternion.Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }
    }
}
