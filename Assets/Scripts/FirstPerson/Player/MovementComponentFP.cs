using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementComponentFP : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float gravity = -9.8f;

    private float fallVelocity = 0;

    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Transform body;
    private bool jumpRequest = false;

    private Vector3 playerMovement;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        SetGavity();
        Jump();
        Move();
        playerMovement.y = fallVelocity;
        characterController.Move(playerMovement * Time.fixedDeltaTime);
    }

    private void Move()
    {
        playerMovement.x = moveInput.x * movementSpeed;
        playerMovement.z = moveInput.y * movementSpeed;
        playerMovement = body.TransformDirection(playerMovement);
    }
    private void SetGavity()
    {
        if (characterController.isGrounded)
        {
            fallVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            fallVelocity += gravity * Time.fixedDeltaTime;
        }
    }
    private void Jump()
    {
        if (jumpRequest)
        {
            fallVelocity = jumpForce;
            jumpRequest = false;
        }
    }

    public void SetJumpRequest()
    {
        if (characterController.isGrounded) jumpRequest = true;
    }
}
