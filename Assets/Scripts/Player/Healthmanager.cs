using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float knockbackForce = 1f;
    [SerializeField] private float knockbackTime = 0.2f;
    [SerializeField] private float attackCoolDownTime = 1f;
    [SerializeField] private CoinsUI coinsUI;

    private MovementComponent movementComponent;
    private CharacterController characterController;

    private int currentHealth = 0;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        movementComponent = GetComponent<MovementComponent>();
        Heal(startHealth);
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;
        if (movementComponent != null) Knockback();
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        coinsUI.ActualizeUI(currentHealth);
        if (currentHealth <= 0) Death();
    }

    private void Knockback()
    {
        Vector3 playerMovementDirection = movementComponent.playerMovement.normalized;
        Vector3 knockback = -playerMovementDirection * knockbackForce;
        bool ground = !movementComponent.onAir;
        canTakeDamage = false;
        movementComponent.canTakeInputs = false;
        StartCoroutine(LifeCoolDown());
        LeanTween.moveX(gameObject, transform.position.x + knockback.x, knockbackTime)
                 .setOnComplete(() => {
                     returnMove();
                  });
        if (ground || playerMovementDirection.y > 0) return;
        LeanTween.moveY(gameObject, transform.position.y + knockback.y, knockbackTime)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    IEnumerator LifeCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDownTime);
        canTakeDamage = true;
    }

    private void returnMove()
    {
        movementComponent.canTakeInputs = true;
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        coinsUI.ActualizeUI(currentHealth);
    }

    public void Death()
    {
        // Aquí iría la lógica de muerte (animaciones, efectos, etc.)
        Debug.Log("Player Died");
    }
}
