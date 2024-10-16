using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(CodeAudioEmitter))]
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private bool canInteract = false;
    private IInteractable interactIcon;
    protected CodeAudioEmitter soundEmitter;
    protected bool active = true;

    private void Awake()
    {
        soundEmitter = GetComponent<CodeAudioEmitter>();
    }

    void Update()
    {
        if (!canInteract){return;}
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            canInteract = true;
            interactIcon = collision.transform.GetChild(1).gameObject.GetComponent<IInteractable>();
            interactIcon.Interact();
        }
    }

    protected virtual void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && interactIcon != null)
        {
            StopInteraction();
        }
    }

    protected void StopInteraction()
    {
        canInteract = false;
        interactIcon.Interact();
        interactIcon = null;
    }

    public virtual void Interact()
    {
    }
}
