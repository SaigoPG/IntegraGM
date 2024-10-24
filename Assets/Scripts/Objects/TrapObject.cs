using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] private int damage;

    public void Attack(HealthManager healthManager)
    {
            healthManager.TakeDamage(damage);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Attack(collision.gameObject.GetComponent<HealthManager>());
            return;
        }
        print("Colisionando con algo");
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Attack(collision.gameObject.GetComponent<HealthManager>());
            return;
        }
        print("Colisionando con algo");
    }
}
