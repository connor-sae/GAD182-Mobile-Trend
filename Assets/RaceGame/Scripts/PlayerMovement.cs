using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 7f;
    public float sidewaysSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //raycast to check if the player is grounded
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        //player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        //always move forward
        Vector3 forwardMove = transform.forward * forwardSpeed;

        //player manually moves sideways
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 strafeMove = transform.right * horizontal * sidewaysSpeed;

        //combine forward and sideways movement
        Vector3 move = new Vector3(strafeMove.x, rb.velocity.y, forwardMove.z);
        rb.velocity = transform.TransformDirection(move);

        rb.velocity = move;
    }
}
