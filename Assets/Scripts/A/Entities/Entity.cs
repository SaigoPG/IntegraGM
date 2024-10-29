using UnityEngine;
public abstract class Entity:MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    protected abstract void Movement();
}
