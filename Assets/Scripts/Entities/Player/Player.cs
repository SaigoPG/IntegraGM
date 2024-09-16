using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : DamageableEntity, IHealable
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastSize = 1f;

    private bool jumpRequest = false;
    private float moveInput;
    private float animationInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInputs();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    protected override void Movement()
    {
        SetGavity();
        Vector3 playerMovement = Vector3.zero;
        playerMovement.x = HandleWalk();
        HandleJump();
        HandleHeadCollisions();
        playerMovement.y = fallVelocity;
        characterController.Move(playerMovement * Time.fixedDeltaTime);
    }

    public override void Death()
    {
        //Ejecutar animacion sonido o lo que sea que ocurra cuando muera
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
    //Funciones del manejo del movimiento y salto

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
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            jumpRequest = true;
            //Forzar animacion de salto
            animator.Play("Jumping", 0);
        }
    }

    private void HandleAnimations()
    {
        animator.SetBool("IsJumping", !characterController.isGrounded && fallVelocity > 0);
        animator.SetFloat("movement", animationInput);
        animator.SetBool("OnFloor", characterController.isGrounded);
        animator.SetBool("IsFalling", !characterController.isGrounded && fallVelocity < 0);
        if (animationInput < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (animationInput > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
