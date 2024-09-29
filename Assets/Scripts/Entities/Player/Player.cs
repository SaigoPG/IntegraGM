using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : DamageableEntity
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastSize = 1f;
    [Header("Damage Params")]
    [SerializeField] private float pushFactor;

    Vector3 platformVelocity, pushDirection;
    private bool jumpRequest = false;
    private float moveInput;
    private float animationInput;
    private bool isOnPlatform;   
    private PlayerAnimator playerAnimator;
    private HealthManager hm;

    [Header("Abilities")]
    public bool doubleJump = false;

    private int jumpsOnAir = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        hm = GetComponent<HealthManager>();
    }

    private void Update()
    {
        HandleInputs();
        playerAnimator.UpdateAnimations(animationInput, fallVelocity, characterController.isGrounded);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    protected override void Movement()
    {
        base.Movement();
        Vector3 playerMovement = Vector3.zero;
        playerMovement.x = HandleWalk();
        HandleJump();
        HandleHeadCollisions();
        playerMovement.y = fallVelocity;

        if(isOnPlatform){

            playerMovement += platformVelocity;

        }

        if(hm.GetAttackStatus()){
           
            pushDirection = hm.GetPushDirection();
            playerMovement += pushDirection * pushFactor;

        }

        characterController.Move(playerMovement * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        if (jumpRequest)
        {
            fallVelocity = jumpForce;
            jumpRequest = false;
        }
    }
    
    private float HandleWalk()
    {
        float XMovement = moveInput * movementSpeed;
        return XMovement;
    }

    private void HandleHeadCollisions()
    {
        Ray ray = new Ray(transform.position, Vector3.up * raycastSize);
        Debug.DrawRay(ray.origin, ray.direction * raycastSize);
        RaycastHit[] hits = Physics.RaycastAll(ray, raycastSize);
        if (hits.Length == 0){return;}
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                fallVelocity = gravity * Time.fixedDeltaTime;
                break;
            }
        }
    }

    private void HandleInputs()
    {
        //Movimiento
        moveInput = Input.GetAxis("Horizontal");
        //Animaciones
        animationInput = Input.GetAxisRaw("Horizontal");
        //Salto
        if (characterController.isGrounded)
        {
            jumpsOnAir = 0;
            if (Input.GetKeyDown(KeyCode.Space)) jumpRequest = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump && jumpsOnAir < 1)
        {
            jumpRequest = true;
            jumpsOnAir = 1;
        }
    }

    //Utility
    
    void OnControllerColliderHit(ControllerColliderHit hit){

        GameObject hitObj = hit.gameObject;
        MovingPlatform mp = hitObj.GetComponent<MovingPlatform>();

        if(mp != null){            

            isOnPlatform = true;
            platformVelocity = mp.GetVelocity();

        } else {

            isOnPlatform = false;   

        }

    }
}
