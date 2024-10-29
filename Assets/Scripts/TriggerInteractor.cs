using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractor : MonoBehaviour
{
    [SerializeField] private List<NotInteractableObject> notInteractableObjects = new List<NotInteractableObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (notInteractableObjects.Count == 0) return;
            foreach (NotInteractableObject obj in notInteractableObjects)
            {
                if (obj.active) obj.Interact();
            }
            Destroy(gameObject);
        }
    }
}
