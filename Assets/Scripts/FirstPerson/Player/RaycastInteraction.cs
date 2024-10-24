using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class RaycastInteraction : MonoBehaviour
{

    [SerializeField] float raycastSize;
    [SerializeField] MenuManager inGameFPManager;

    private bool collidingWithInteractableObject;
    RaycastHit hit;
    InteractableObject iObject;

    private bool oneChange = true;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * raycastSize, Color.red);

        // Usa raycastSize como la distancia del rayo, no en la dirección
        if (Physics.Raycast(ray, out hit, raycastSize))
        {
            if (hit.collider.TryGetComponent(out InteractableObject interactableObject))
            {
                iObject = interactableObject;
                collidingWithInteractableObject = true;
                inGameFPManager.Transition("Select");
                if (oneChange)
                {
                    oneChange = false;
                }
                return;
            }
        }

        collidingWithInteractableObject = false;
        iObject = null;
        if (!oneChange)
        {
            inGameFPManager.Transition("Dot");
            oneChange = true;
        }
    }


    public void ExecuteInteraction()
    {
        if (!collidingWithInteractableObject || iObject == null) return;
        iObject.Interact();
    }
}
