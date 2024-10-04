using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TrapObject : MonoBehaviour, IAttacker
{
    [SerializeField] private int damage;

    private Vector3 attackNormal;

    public void Attack(IDamageable damagableEntity)
    {
            damagableEntity.TakeDamage(damage, attackNormal);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            
            print("Colisionando con jugador");
            Vector3 colPoint = collision.ClosestPoint(transform.position);
            attackNormal = transform.position - colPoint;

            Attack(collision.gameObject.GetComponent<IDamageable>());
            
            
            return;
        }
        print("Colisionando con algo");
    }

    private void OnTriggerExit(Collider col){

        if(col.gameObject.GetComponent<HealthManager>() != null){

            col.gameObject.GetComponent<HealthManager>().SetAttackStatus(false);

        }

    }
}
