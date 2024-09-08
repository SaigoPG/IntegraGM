using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : DamageableEntity, IHealable
{
    [SerializeField] private float jumpForce;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity = jumpForce;
        }
    }
    
    private float HandleWalk()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float XMovement = moveInput * movementSpeed;
        return XMovement;
    }

}
