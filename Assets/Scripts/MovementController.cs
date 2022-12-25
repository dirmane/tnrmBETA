using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // movement speed
    public float speed = 5.0f;

    //defining what is his floor
    public LayerMask groundLayer;

    //flor detection
    public float groundDetectionRadius = 0.1f;
    public float groundDistance = 0.5f;

    // rotation smoothness
    public float rotationSmoothness = 10.0f;

    //animator 
    private Animator animator;

    //rigid body
    private Rigidbody rb;



    void Start()
    {

        rb = GetComponent<Rigidbody>();
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

            // get the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // smooth out the rotation using Quaternion.Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }
        if (verticalInput != 0 || horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }

    void FixedUpdate()
    {
        // check if the character is on the ground
        bool isOnGround = Physics.SphereCast(transform.position, groundDetectionRadius, -transform.up, out RaycastHit hit, groundDistance, groundLayer);

        // if the character is not on the ground, apply gravity
        if (!isOnGround)
        {
            rb.AddForce(Physics.gravity * rb.mass);
        }
    }
}
