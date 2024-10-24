using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpPowerUp : PowerUpBase
{
    private void Update()
    {
        if (movementComponent == null) return;
        if (!movementComponent.onAir && powerUpSpent) ReloadPowerUp();
    }

    public override void ActivePowerUp()
    {
        base.ActivePowerUp();
        
        movementComponent.SetJumpRequest(false);
    }
    public override void ReloadPowerUp()
    {
        base.ReloadPowerUp();
        print("Salto recargado");
    }
}
