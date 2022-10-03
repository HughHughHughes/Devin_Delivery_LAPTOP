using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFP : MonoBehaviour
{
    public GameObject player;
    public bool boostActive = false;
    [Header("GameManager/StatTracker")]
    [SerializeField] GameObject refToGameManager;
    [Header("Orientation")]
    [SerializeField] Transform orientation;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f; //when not grounded

    public Vector3 _playervelocity;

    float horizontalMovement;
    float verticalMovement;
    Vector3 moveDirection;

    public bool resetRBaddVel;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public bool playerJumpFromBombImpact; // if player is grounded this happens
    public bool bombEffectAirDrag;
    public float bombAirMovementMultiplier = 5;
    //

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 1f;

    public float currentDownForceWhenInAir;
    public float maximumDownForceWhenInAir;

    [Header("MidAirRocketJumpVars")]
    public bool m_oneTime = false;
    public float tickyTimer;


    public bool youShotAndHitYou = false;

    public bool hitPlatAddForceRight = false;
    public bool hitPlatAddForceLeft = false;

    public bool m_twoTime = false;
    public float tempTimerToResetPlayerJumpFromBombImpact;


    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    float playerHeight = 3f; //forraycast ground detect.
    public bool isGrounded;
    public float inAirTime;
    float groundDistance = 0.4f;

    Rigidbody rb;

    public float velx;
    public float velz;
    public float vely;



   

    private void Awake()
    {
        player = GameObject.Find("Player");
        refToGameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _playervelocity = rb.velocity;

    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 1.5f, 0), 0.4f);
    }

    private void Update()
    {
        //
        Physics.IgnoreLayerCollision(12, 13);
        Physics.IgnoreLayerCollision(13, 9);
       

        _playervelocity = rb.velocity;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance + 0.5f, groundMask);
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance + 1.5f, groundMask);

        MyInput();
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        GunImpact();
        ControlDrag();
        VelocityLimits();
    }

    void ifGunEquipped()
    {

    }
   

    void MyInput()
    {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement; // move in direction relative to where player facing
    }
    void Jump()
    {
        if (!isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //impuse adds sudden force relative to its mass.
        }
    }
    void GunImpact()
    {
        Physics.IgnoreLayerCollision(12, 13);
    }
    void ControlDrag()
    {

        //air drag we want minimual, so player doesnt feel like theyre floating in air, snappy movmeent
        if (isGrounded)
        {
            rb.drag = groundDrag;
            inAirTime = 0;
            currentDownForceWhenInAir = 0;


        }
        if (isGrounded == false)
        {


            rb.drag = airDrag;

            currentDownForceWhenInAir += 8 * Time.deltaTime;


            inAirTime += Time.deltaTime;

            rb.AddForce(0, -currentDownForceWhenInAir, 0); //testing april 2022

            if (currentDownForceWhenInAir >= maximumDownForceWhenInAir)
            {
                currentDownForceWhenInAir = maximumDownForceWhenInAir;
            }

        }



    }

    private void FixedUpdate()
    {
        MovePlayer(); //rb movement better in fixed update.
    }
    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        else if (!isGrounded)
        {
            if (!bombEffectAirDrag)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            }

            if (bombEffectAirDrag)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * bombAirMovementMultiplier, ForceMode.Acceleration);
            }
        }
    }
    void VelocityLimits()
    {

        velx = rb.velocity.x;
        velz = rb.velocity.z;
        vely = rb.velocity.y;

        if (velz >= 50)
        {
            velz = 50;
        }
        if (velz <= -50)
        {
            velz = -50;
        }
        if (velx >= 50)
        {
            velx = 50;
        }
        if (velx <= -50)
        {
            velx = -50;
        }

        if (vely >= 60)
        {
            vely = 60;
        }

        if (vely <= -30)
        {
            vely = -30;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (verticalMovement == 1)
        {
            verticalMovement = 0;
        }
        if (collision.collider.tag == "Ground")
        {
            //transform.parent = collision.transform;
            isGrounded = true;
            bombEffectAirDrag = false;
            currentDownForceWhenInAir = 0;
        }


    }
    private void OnCollisionStay(Collision collision)
    {


    }
    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;

        if (collision.collider.tag == "Ground")
        {
            inAirTime = 0;
        }

    }
    private void OnTriggerEnter(Collider trig)
    {
        if (trig.GetComponent<Collider>().tag == "Ground")
        {
            //transform.parent = trig.transform;

          
            bombEffectAirDrag = false;
           
        }

       
    }
    private void OnTriggerStay(Collider trig)
    {
        if (trig.GetComponent<Collider>().tag == "Ground")
        { 
            //transform.parent = trig.transform;
        }
    }

    private void OnTriggerExit(Collider trig)
    {
        transform.parent = null;
    }

}