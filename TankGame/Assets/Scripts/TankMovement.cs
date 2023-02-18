using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    /* variables */
    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed;

    public float groundDrag;

    [Header("IsOnGround")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }
    //Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); // used to check if we are on the ground
        PlayerInput();

        if (grounded) // apply friction if we are on the ground
            rigidBody.drag = groundDrag;
        else
            rigidBody.drag = 0;

        SpeedControl();



    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movement direction
        //moveDirection = orientation.forward * verticalInput; // get tank's forward and right orientation and combine them to move tank in specified direction

        rigidBody.AddForce(rigidBody.transform.forward * (moveSpeed * 10f) * (verticalInput * Time.deltaTime), ForceMode.Force); // apply the calculate force
        rigidBody.transform.Rotate(Vector3.up, (rotationSpeed * horizontalInput) * Time.deltaTime); // att rotation to the tank based on horizontal input
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z); // default velocity of the tank

        //limit velocity if needed

        if(flatVel.magnitude > moveSpeed) // if the tanks velocity is greater than its set movement speed
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // adjust tank speed to match move speed
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z); // limit the current velocity of the tank to the desired velocity set 
        }

    }
}
