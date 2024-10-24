using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotInteractableObject : MonoBehaviour
{
    [SerializeField] private string interactSoundName;
    [HideInInspector] public bool active { get; protected set; } = true;

    private void Awake()
    {
        _Awake();
    }

    private void Start()
    {
        _Start();
    }

    private void Update()
    {
        _Update();
    }

    private void FixedUpdate()
    {
        _FixedUpdate();
    }

    protected abstract void _Awake();
    protected abstract void _Start();
    protected abstract void _Update();
    protected abstract void _FixedUpdate();

    public virtual void Interact(){}
}
