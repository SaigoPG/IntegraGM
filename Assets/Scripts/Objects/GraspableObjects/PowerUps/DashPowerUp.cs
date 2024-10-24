using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerUp : PowerUpBase
{

    [SerializeField] private float DashVelocity = 50f;
    [SerializeField] private float DashTime = 0.1f;
    [SerializeField] private float DashCooldown = 0.5f;

    private float normalVelocity;
    private Coroutine dashCoroutine;
    private bool canReload = false;
    private bool touchFloor = false;

    private void Update()
    {
        if (movementComponent == null) return;
        if (!powerUpSpent) return;
        if (!movementComponent.onAir && !touchFloor) touchFloor = true;
        else if (touchFloor && canReload) ReloadPowerUp();
    }

    public override void ActivePowerUp()
    {
        touchFloor = false;
        base.ActivePowerUp();
        normalVelocity = movementComponent.movementSpeed;
        movementComponent.movementSpeed = DashVelocity;
        StartCoyoteCoroutine();
        LeanTween.value(gameObject, movementComponent.movementSpeed, normalVelocity, DashTime)
                 .setOnUpdate((float value) => {
                     movementComponent.movementSpeed = value;
                 });
    }
    public override void ReloadPowerUp()
    {
        base.ReloadPowerUp();
        canReload = false;
    }

    IEnumerator DashTimeProcess()
    {
        yield return new WaitForSeconds(DashCooldown);
        canReload = true;
    }

    void StartCoyoteCoroutine()
    {
        dashCoroutine = StartCoroutine(DashTimeProcess());
    }
}
