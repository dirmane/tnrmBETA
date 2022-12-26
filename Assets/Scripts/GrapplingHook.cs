using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float hookRange = 10.0f; // The maximum range of the grappling hook
    public float hookSpeed = 10.0f; // The speed at which the grappling hook travels
    public LayerMask hookMask = Physics.AllLayers;
    public LineRenderer lineRenderer; // The Line Renderer component that will render the grappling hook's line
    public Transform hookPoint; // The point at which the grappling hook will be anchored

    private bool isHooked; // Whether or not the grappling hook is currently hooked to something
    private Vector3 hookPosition; // The position of the grappling hook's anchor point
    private Rigidbody rb; // The Rigidbody component attached to the player

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is pressing the "Fire" button
        if (Input.GetButtonDown("Fire1"))
        {
            // The player is pressing the "Fire" button, so shoot the grappling hook
            StartCoroutine(ShootHook());
        }

        // Check if the grappling hook is currently hooked to something
        if (isHooked)
        {
            // The grappling hook is hooked to something, so update the hook's position
            hookPoint.position = hookPosition;

            // Calculate the player's movement vector based on the distance between the player and the hook point
            Vector3 move = hookPoint.position - transform.position;

            // Normalize the movement vector
            move = move.normalized;

            // Update the player's velocity
            rb.velocity = move * hookSpeed;
        }
    }

    IEnumerator ShootHook()
    {
        // Set the line renderer's positions
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * hookRange);

        // Cast a ray from the player's position in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, hookRange, hookMask))
        {
            // The ray hit something, so set the hook's anchor point to the hit point
            hookPosition = hit.point;
            isHooked = true;

            // Enable the line renderer
            lineRenderer.enabled = true;
        }
        else
        {
            // The ray didn't hit anything, so disable the line renderer
            lineRenderer.enabled = false;
            yield break;
        }

        // Wait until the player releases the "Fire" button
        while (Input.GetButton("Fire1"))
        {
            yield return null;
        }

        // The player has released the "Fire" button, so reset the grappling hook
        isHooked = false;
        lineRenderer.enabled = false;
        hookPoint.position = transform.position;
    }
}
