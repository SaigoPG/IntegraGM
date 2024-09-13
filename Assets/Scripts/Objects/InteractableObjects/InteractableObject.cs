using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private bool canInteract = false;
    private IInteractable interactIcon;
    void Update()
    {
        if (!canInteract){return;}
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        canInteract = true;
        interactIcon = collision.transform.GetChild(1).gameObject.GetComponent<IInteractable>();
        interactIcon.Interact();
    }

    private void OnTriggerExit(Collider collision)
    {
        canInteract = false;
        interactIcon.Interact();
        interactIcon = null;
    }
    public virtual void Interact()
    {
        print("Boton presionado");
    }
}
