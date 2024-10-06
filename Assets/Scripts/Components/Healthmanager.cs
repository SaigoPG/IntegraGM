using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable, IHealable, IDestroyable
{
    [SerializeField] private int maxHealth = 100;
    [Header("Damage Effect")]
    [SerializeField] private int numberOfFlashes;
    private int currentHealth;
    public bool canTakeDamage = true;
    private Vector3 pushDirection;
    private bool isBeingAttacked;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 dmgDirection)
    {
        if (!canTakeDamage) return;
        isBeingAttacked = true;
        pushDirection = -dmgDirection;
        pushDirection.z = 0;
        pushDirection.y *= 20;
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);       
        StartCoroutine(DamageFlickering());
        print(currentHealth);
        if (currentHealth <= 0) Death();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void Death()
    {
        // Aqu� ir�a la l�gica de muerte (animaciones, efectos, etc.)
        Debug.Log("Player Died");
    }

    IEnumerator DamageFlickering(){
        

        for (int counter = 0; counter < numberOfFlashes; counter++){

            GameObject skin = gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
            GameObject joints = gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;

            SkinnedMeshRenderer skinMr = skin.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer jointsMr = joints.GetComponent<SkinnedMeshRenderer>();

            skinMr.enabled = false;
            jointsMr.enabled = false;
            yield return new WaitForSeconds(.2f);
            skinMr.enabled = true;
            jointsMr.enabled = true;
            yield return new WaitForSeconds(.2f);

        }

    }

    public Vector3 GetPushDirection(){

        return pushDirection;

    }

    public bool GetAttackStatus(){

        return isBeingAttacked;

    }

    public void SetAttackStatus(bool newAttackStatus){

        isBeingAttacked = newAttackStatus;

    }
}
