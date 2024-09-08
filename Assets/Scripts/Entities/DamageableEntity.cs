using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntity : Entity, IDamageable, IDestroyable
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float gravity = -9.8f;
    [HideInInspector] public bool canTakeDamage { get; set; } = true;

    protected int health;
    protected CharacterController characterController;
    protected float fallVelocity = 0;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Death();
        }
        print(health);
    }

    protected void SetGavity()
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
    public abstract void Death();

}
