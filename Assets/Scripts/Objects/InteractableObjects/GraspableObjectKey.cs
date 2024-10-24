using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspableObjectKey : InteractableObject
{
    [SerializeField] private GraspableObject graspableObject;

    private InventoryComponent playerInventory;

    protected override void _Awake() { }

    protected override void _Start() { }

    protected override void _Update() { }

    protected override void _FixedUpdate() { }

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            playerInventory = collision.gameObject.GetComponent<InventoryComponent>();
        }
    }

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
