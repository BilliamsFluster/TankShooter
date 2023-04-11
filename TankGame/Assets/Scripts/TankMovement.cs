using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    /* variables */
    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed = 50f;
    public float maxRotationSpeed = 50f;
    public float minRotationSpeed = 20f;
    float collisionRadius = 2;
    public string detectedObjectForRotationMod;
    public LayerMask collisionFilter;
    float speedFact = 0;
    [SerializeField] private string inputHorizontal;
    [SerializeField] private string inputVertical;




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
        bool hasCollided = Physics.CheckSphere(transform.position, collisionRadius, collisionFilter);
        
        if (hasCollided)
        {
            RotationControl(true);
            
        }
        else
        {
            RotationControl(false);
        }
       
        
        PlayerInput();

        if (grounded) // apply friction if we are on the ground
            rigidBody.drag = groundDrag;
        else
            rigidBody.drag = 0;

        SpeedControl();




    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
    public void RotationControl(bool SeeObject)
    {
        
        if(SeeObject)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);

            foreach (Collider collider in colliders)
            {
                Debug.Log(collider.gameObject.layer.ToString());
                if (collider.gameObject.tag == detectedObjectForRotationMod)
                {

                    Debug.Log("got layer");
                    float distanceFromObject = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                    float speedFactor = (distanceFromObject / collisionRadius);
                    speedFact = 1/speedFactor;
                    rotationSpeed = Mathf.Lerp(maxRotationSpeed,minRotationSpeed, 1/speedFactor);  
                }
            }

        }
        else
        {
            speedFact = 0;
            rotationSpeed = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, speedFact);
            
        }


    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis(inputHorizontal);
        verticalInput = Input.GetAxis(inputVertical);
    }

    private void MovePlayer()
    {
       
        rigidBody.AddForce(rigidBody.transform.forward * (moveSpeed * 10f) * (verticalInput * Time.deltaTime), ForceMode.Force); // apply the calculate force
        rigidBody.transform.Rotate(Vector3.up, (rotationSpeed * horizontalInput) * Time.deltaTime); // at rotation to the tank based on horizontal input
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
