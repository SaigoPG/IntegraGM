using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSwitchFP : InteractableSwitchFP
{
    [SerializeField] private string key;
    [SerializeField] private string notKeySound;

    private InventoryComponent playerInventory;
    protected override void _Awake()
    {
        base._Awake();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<InventoryComponent>();
    }

    public override void Interact()
    {
        GraspableObject keyObject = playerInventory.FindObject(key, false);

        if (keyObject != null)
        {
            base.Interact();
        }
        else
        {
            Debug.Log("No se tiene el objeto " + key + " para abrir la puerta");
            soundEmitter.emitSound(notKeySound);
        }
    }
}