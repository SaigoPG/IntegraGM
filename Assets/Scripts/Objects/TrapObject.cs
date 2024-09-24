using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour, IAttacker
{
    [SerializeField] private int damage;

    public void Attack(IDamageable damagableEntity)
    {
            damagableEntity.TakeDamage(damage);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Colisionando con jugador");
            Attack(collision.gameObject.GetComponent<IDamageable>());
            return;
        }
        print("Colisionando con algo");
    }
}
