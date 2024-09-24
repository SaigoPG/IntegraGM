using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable, IHealable, IDestroyable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public bool canTakeDamage = true;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        print(currentHealth);
        if (currentHealth <= 0) Death();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void Death()
    {
        // Aquí iría la lógica de muerte (animaciones, efectos, etc.)
        Debug.Log("Player Died");
    }
}
