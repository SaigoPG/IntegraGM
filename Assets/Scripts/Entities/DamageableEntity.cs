using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntity : Entity, IDamageable
{
    [SerializeField] protected int maxHealth;

    protected int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Die();
        }
    }
    protected abstract void Die();
}
