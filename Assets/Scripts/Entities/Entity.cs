using UnityEngine;
public abstract class Entity:MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    protected abstract void Movement();
    public abstract float GetMovementSpeed();
    public abstract void SetMovementSpeed(float newSpeed);
}
