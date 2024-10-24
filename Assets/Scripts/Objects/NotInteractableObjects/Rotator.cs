using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : NotInteractableObject
{
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private float rotationAmount = 130f;
    [SerializeField] private RotationAxis rotationDirection;

    private Collider colision;
    private Vector3 initialRotation;
    private bool hasRotated = false;
    private GameObject parent;

    private enum RotationAxis
    {
        X,
        lessX,
        Y,
        lessY,
        Z,
        lessZ
    }

    protected override void _Awake() { colision = GetComponent<Collider>(); }

    protected override void _Start() { parent = transform.parent.gameObject; }

    protected override void _Update() { }

    protected override void _FixedUpdate() { }

    public override void Interact()
    {
        active = false;
        colision.enabled = false;
        Vector3 finalRotation = setAxis() * rotationAmount;
        LeanTween.rotateLocal(parent, hasRotated ? initialRotation : finalRotation, animationTime).setEase(LeanTweenType.easeOutExpo).setOnComplete(Reactivate);
        hasRotated = !hasRotated;
    }

    private void Reactivate()
    {
        active = true;
        colision.enabled = true;
    }

    private Vector3 setAxis()
    {
        switch (rotationDirection)
        {
            case RotationAxis.X:
                return new Vector3(1, 0, 0);
            case RotationAxis.Y:
                return new Vector3(0, 1, 0);
            case RotationAxis.Z:
                return new Vector3(0, 0, 1);
            case RotationAxis.lessX:
                return new Vector3(-1, 0, 0);
            case RotationAxis.lessY:
                return new Vector3(0, -1, 0);
            case RotationAxis.lessZ:
                return new Vector3(0, 0, -1);
            default:
                return new Vector3(0, 0, 0);
        }
    }

}

