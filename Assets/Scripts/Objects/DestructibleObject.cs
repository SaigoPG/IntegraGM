using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable, IDestroyable
{
    [HideInInspector] public bool canTakeDamage { get; set; } = true;

    public void TakeDamage(int damage)
    {

    }

    public void Death()
    {

    }
}
