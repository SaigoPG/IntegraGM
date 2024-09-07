using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public abstract class Entity:MonoBehaviour
{
    protected Rigidbody rigidbody;
    protected abstract void Movement();
}
