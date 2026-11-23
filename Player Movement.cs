using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

//This script allows you to move around jump crouch walk and sprint
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sneek = 3.0f;
    public float jump = 5.0f;
    public float run = 8.0f;

    private Vector2 movementInput;

    private float currentSpeed;
    private bool isSneeking;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump = true;
    private bool wasAirborne;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Gets the player compenent 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    //checks if you are "sneeking" running or walking
    void FixedUpdate()
    {


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        if (!isGrounded)
        {
            wasAirborne = true;
        }
        else if (wasAirborne)
        {
            canJump = true;
            wasAirborne = false;
        }


        if(isSneeking == true)
        {
            currentSpeed = sneek;
        }
        else if(isRunning)
        {
            currentSpeed = run;
        }
        else
        {
            currentSpeed = speed;
        }
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        rb.linearVelocity = new Vector3(move.x * currentSpeed, rb.linearVelocity.y, move.z * currentSpeed);
    }

    public void OnMove(InputValue aValue)
    {
        movementInput = aValue.Get<Vector2>();
    }

    public void OnSneek(InputValue aValue)
    {
        isSneeking = aValue.isPressed; //is pressed is so sneeking happens when its pressed and stops when the key is no longer pressed
    }

    public void OnRun(InputValue aValue)
    {
        isRunning = aValue.isPressed;
    }
    //allows you to jump
    public void OnJump(InputValue aValue)
    {
        if(aValue.isPressed && canJump && isGrounded)
        {
            canJump = false;
            wasAirborne = true;
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);

        }
    }
}
