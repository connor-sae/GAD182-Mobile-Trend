using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sidewaysSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f; // Adjust based on player height

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Simple raycast to check if we're grounded
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // Handle jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Auto move forward
        Vector3 move = transform.forward * forwardSpeed;

        // Side movement (A/D or Left/Right arrow)
        float sideways = Input.GetAxis("Horizontal");
        move += transform.right * sideways * sidewaysSpeed;

        // Keep vertical velocity for jumping/gravity
        move.y = rb.velocity.y;

        // Apply movement
        rb.velocity = move;
    }
}
