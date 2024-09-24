using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour, IAttacker
{
    [SerializeField] private int damage;
    [SerializeField] private string debuffType;
    [SerializeField] private float slowFactor;

    public void Attack(IDamageable damagableEntity)
    {
        if (damagableEntity.canTakeDamage)
        {
            damagableEntity.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        GameObject colObj = collision.gameObject;

        if (colObj.CompareTag("Player"))
        {   
            Player playerRef = colObj.GetComponent<Player>();

            switch (debuffType){

                case ("SlowDown"):

                    Debug.Log("SlowDown");
                    playerRef.SetMovementSpeed(playerRef.GetMovementSpeed() * slowFactor);

                    break;

                case ("Damaging"):

                    Debug.Log("Damaging");
                    Attack(colObj.GetComponent<IDamageable>());
                    playerRef.SetJumpRequest(true);                  

                    break;

                case ("Both"):

                    Debug.Log("Both");
                    playerRef.SetMovementSpeed(playerRef.GetMovementSpeed() * slowFactor);
                    Attack(colObj.GetComponent<IDamageable>());
                    playerRef.SetJumpRequest(true);  

                    break;

                default:
                    Debug.Log("What");
                    break;

            }
            
        }
        
    }
}
