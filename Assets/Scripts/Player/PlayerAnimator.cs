using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private MovementComponent movementComponent;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateAnimations(movementComponent.moveInput, movementComponent.fallVelocity, characterController.isGrounded);
    }

    public void UpdateAnimations(float animationInput, float fallVelocity, bool isGrounded)
    {
        animator.SetBool("IsJumping", !isGrounded && fallVelocity > 0);
        animator.SetFloat("movement", animationInput);
        animator.SetBool("OnFloor", isGrounded);
        animator.SetBool("IsFalling", !isGrounded && fallVelocity < 0);

        // Voltear el personaje dependiendo de la dirección
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
