using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAgent : Agent
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
    public GameLogic GameLogic;
    private float horizontalInput;
    private float verticalInput;
    private bool readyToJump;

    private int rotationDirection;
    public float turnSpeed;

    public override void OnEpisodeBegin()
    {
        GameLogic.ResetAll();
        
        this.playerHeight = this.transform.localScale.y;
        this.transform.localPosition = new Vector3(0, 0.5f, -1.78f);
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
        readyToJump = true;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       sensor.AddObservation(grounded);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        float horizInput = actions.ContinuousActions[0];
        float vertInput = actions.ContinuousActions[1];
        int jump = actions.DiscreteActions[0];
        int moveRotation = actions.DiscreteActions[1];

        if (jump == 1 && grounded && readyToJump)
        {
            AddReward(-0.1f);
        }
        
        InputFunction(horizInput, vertInput, jump,moveRotation);
        SpeedControl();
        if (grounded)
        {
            myRigidbody.drag = groundDrag;
        }
        else
            myRigidbody.drag = 0; 
        MovePlayer();
        rotateAgent(rotationDirection);
        AddReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[1] = Input.GetAxisRaw("Vertical");

        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        
        if (Input.GetKey(KeyCode.Q))
        {
            discreteActionsOut[1] = 0;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            discreteActionsOut[1] = 1;
        }
        else
        {
            discreteActionsOut[1] = 2;
        }
        
    }

    private void MovePlayer()
    {
        // calculate movement direction
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // on ground
        if(grounded)
            myRigidbody.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            myRigidbody.AddForce(movementDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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
    
    private void InputFunction(float horizInput, float vertInput, int jump, int moveRotation)
    {
        horizontalInput = horizInput;
        verticalInput = vertInput;
        // when to jump

        rotationDirection = moveRotation;
        
        if(jump == 1 && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void rotateAgent(int moveRotation)
    {
        if (moveRotation == 0)
        {
            transform.Rotate(Time.deltaTime * turnSpeed * Vector3.down);
        }
        else if (moveRotation == 1)
        {
            transform.Rotate(Time.deltaTime * turnSpeed * Vector3.up);
        }
    }
    
    
    private void Jump()
    {
        // reset y velocity
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.name == "O8 Part 2"|| collision.transform.name=="O24 Part 1")
        {
            Lose();
        }
        
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void PressurePlatePress()
    {
       
        AddReward(1f);
        Debug.Log(GetCumulativeReward());
    }

    public void Win()
    {
        AddReward(2.5f);
        EndEpisode();
        
    }

    public void Lose()
    {
        AddReward(-0.5f);
        EndEpisode();
    }
    
}
