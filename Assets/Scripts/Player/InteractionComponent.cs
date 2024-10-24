using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private GameObject interactIcon;
    [SerializeField] private float interactIconAnimationTime = 0.2f;

    private List<InteractableObject> interactableObjects = new List<InteractableObject>();

    private void Awake()
    {
        interactIcon.transform.localScale = new Vector3(0,0,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactable))
        {
            if (!interactable.active) return;
            interactableObjects.Add(interactable);
            ShowInteractableObjects();
            if (interactableObjects.Count == 1) interactIconActivation(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactable))
        {
            if (interactableObjects.Contains(interactable))
            {
                interactableObjects.Remove(interactable);
                ShowInteractableObjects();
                if (interactableObjects.Count == 0) interactIconActivation(false);
            }
        }
    }

    private void ShowInteractableObjects()
    {
        string listContent = string.Join(", ", interactableObjects);
        Debug.Log("Lista de Interactable objects: " + listContent);
    }

    private void interactIconActivation(bool active)
    {
        if (active) LeanTween.scale(interactIcon, new Vector3(1, 1, 1), interactIconAnimationTime).setEaseOutExpo();
        else LeanTween.scale(interactIcon, new Vector3(0, 0, 1), interactIconAnimationTime).setEaseOutExpo();
    }

    public void ExecuteInteraction()
    {
        if (interactableObjects.Count <= 0) return;
        InteractableObject lastInteractable = interactableObjects[interactableObjects.Count - 1];
        lastInteractable.Interact();
        if (lastInteractable.active) return;
        interactableObjects.Remove(lastInteractable);
        if (interactableObjects.Count == 0) interactIconActivation(false);
    }

}
