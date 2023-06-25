using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    private Rigidbody myRigidbody;
    private Vector3 movementDirection;
    public float jumpCooldown;

    public LayerMask whatIsGround;
    public float airMultiplier;
    private float playerHeight;
    private bool grounded;

    private float horizontalInput;
    private float verticalInput;
    private bool readyToJump;
    
    void Start()
    {
        this.playerHeight = this.transform.localScale.y;
        this.transform.position = new Vector3(0,0.5f,0);
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
        readyToJump = true;

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        InputFunction();
        SpeedControl();

        if (grounded)
        {
            myRigidbody.drag = groundDrag;
        }
        else
            myRigidbody.drag = 0;
    }

    private void InputFunction()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // when to jump
        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // calculate movement direction
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // on ground
        if(grounded)
            myRigidbody.AddForce(movementDirection.normalized * (moveSpeed * 10f), ForceMode.Force);

        // in air
        else if(!grounded)
            myRigidbody.AddForce(movementDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            myRigidbody.velocity = new Vector3(limitedVel.x, myRigidbody.velocity.y, limitedVel.z);
        }
    }
    
    private void Jump()
    {
        // reset y velocity
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
