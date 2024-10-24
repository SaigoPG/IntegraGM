using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private List<GraspableObject> graspableObjects = new List<GraspableObject>();

    public void AddObject(GraspableObject graspableObject)
    {
        graspableObjects.Add(graspableObject);
        ShowInteractableObjects();
        if (graspableObject.gameObject.TryGetComponent(out PowerUpBase powerUp))
        {
            AssingMoveComponent(powerUp);
        }
    }
    public GraspableObject FindObject(string id, bool isPowerUp)
    {
        if (graspableObjects.Count == 0) return null;
        List<GraspableObject> objectsWithId = graspableObjects.Where(e => e.id == id).ToList();
        if (!isPowerUp) return objectsWithId.FirstOrDefault();
        else
        {
            foreach (var graspableObject in objectsWithId)
            {
                if (graspableObject.gameObject.TryGetComponent(out PowerUpBase powerUp))
                {
                    if (powerUp != null && !powerUp.powerUpSpent)
                    {
                        return powerUp;
                    }
                }
            }
            return null;
        }
    }

    private void ShowInteractableObjects()
    {
        string listContent = string.Join(", ", graspableObjects);
        Debug.Log("Lista de Interactable objects: " + listContent);
    }

    private void AssingMoveComponent(PowerUpBase powerUp)
    {
        MovementComponent movementComponent = GetComponent<MovementComponent>();
        powerUp.SetMovementComponent(movementComponent);

    }
}
