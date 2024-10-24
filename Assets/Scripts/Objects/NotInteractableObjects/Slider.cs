using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : NotInteractableObject
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float animationTime = 1f;

    private Vector3[] positions;
    private int currentPosition = 0;
    protected override void _Awake()
    {
        positions = new Vector3[_positions.Length + 1];
        positions[0] = transform.localPosition;
        for (int i = 0; i < _positions.Length; i++) positions[i + 1] = _positions[i].localPosition;
    }

    protected override void _Start(){}

    protected override void _Update() { }

    protected override void _FixedUpdate() { }

    public override void Interact()
    {
        active = false;
        currentPosition = (currentPosition + 1) % positions.Length;
        LeanTween.moveLocal(gameObject, positions[currentPosition], animationTime).setIgnoreTimeScale(true).setOnComplete(Reactivate);

    }

    private void Reactivate()
    {
        active = true;
    }
}
