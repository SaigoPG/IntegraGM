using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : GraspableObject
{
    protected MovementComponent movementComponent;
    public bool powerUpSpent { get; private set; }

    public void SetMovementComponent(MovementComponent inputmovementComponent)
    {
        movementComponent = inputmovementComponent;
    }

    public virtual void ActivePowerUp()
    {
        if (powerUpSpent) return;
        powerUpSpent=true;
    }
    public virtual void ReloadPowerUp()
    {
        powerUpSpent = false;
    }
}
