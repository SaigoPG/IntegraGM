using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int healAmount;

    HealthManager playerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager = other.GetComponent<HealthManager>();
            playerManager.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
