using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : DamageableEntity, IHealable
{
    [SerializeField] private float jumpForce;

    private bool jumpRequest = false;
    private float moveInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleInputs();
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

    private void HandleInputs()
    {
        //Movimiento
        moveInput = Input.GetAxis("Horizontal");
        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            jumpRequest = true;
        }
    }
}
