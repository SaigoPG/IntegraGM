using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DamageableEntity, IHealable
{
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    protected override void Movement()
    {

    }

    protected override void Die()
    {

    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
}
