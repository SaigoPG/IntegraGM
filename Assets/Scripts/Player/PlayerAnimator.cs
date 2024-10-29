using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private MovementComponent movementComponent;
    private Rigidbody rb;

    // Nueva variable para verificar si está en una plataforma móvil
    public bool isOnMovingPlatform;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateAnimations(movementComponent.moveInput, rb.velocity.y, movementComponent.onAir);
    }

    public void UpdateAnimations(float animationInput, float verticalVelocity, bool isInAir)
    {
        // Determinar si está cayendo
        bool isFalling = isInAir && !isOnMovingPlatform && verticalVelocity < 0;

        animator.SetBool("IsJumping", isInAir && verticalVelocity > 0); // Si está saltando
        animator.SetFloat("movement", animationInput);
        animator.SetBool("OnFloor", !isInAir);
        animator.SetBool("IsFalling", isFalling);

        if (GameManager.Instance.pause) return;

        // Girar el personaje según la dirección del movimiento
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

