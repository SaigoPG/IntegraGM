using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntity : Entity
{
    [SerializeField] protected float _gravity = -9.8f;

    protected CharacterController characterController;
    protected float fallVelocity = 0;

    public float gravity => _gravity;



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

    protected override void Movement()
    {
        SetGavity();
    }

}
