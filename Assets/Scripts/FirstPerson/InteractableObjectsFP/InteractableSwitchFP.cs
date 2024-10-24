using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InteractableSwitchFP : InteractableObject
{
    [SerializeField] private List<NotInteractableObject> notInteractableObjects = new List<NotInteractableObject>();

    protected override void _Awake(){}

    protected override void _Start(){}

    protected override void _Update(){}

    protected override void _FixedUpdate(){}

    public override void Interact()
    {
        base.Interact();
        if (notInteractableObjects.Count == 0) return;
        foreach (NotInteractableObject obj in notInteractableObjects)
        {
            if (obj.active) obj.Interact();
        }
    }
}
