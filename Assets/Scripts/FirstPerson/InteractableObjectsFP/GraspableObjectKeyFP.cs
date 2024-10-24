using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspableObjectKeyFP : InteractableObject
{
    [SerializeField] private GraspableObject graspableObject;

    private InventoryComponent playerInventory;

    protected override void _Awake()
    {
        if (playerInventory == null)
        {
            playerInventory = GameObject.FindWithTag("Player").GetComponent<InventoryComponent>();
        }

        if (playerInventory == null)
        {
            Debug.LogError("Player no está asignado o no tiene un inventario");
        }
    }

    protected override void _Start() { }

    protected override void _Update() { }

    protected override void _FixedUpdate() { }

    public override void Interact()
    {
        if (graspableObject == null)
        {
            print("No se ha asignado el objeto");
            return;
        }
        base.Interact();
        active = false;
        playerInventory.AddObject(graspableObject);
        Destroy(gameObject);
    }
}
