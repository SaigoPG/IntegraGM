using System.Collections;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class TrapObject : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float pushFactor;
    private Vector3 attackNormal;   

    // public void Attack(IDamageable damagableEntity)
    // {
    //         damagableEntity.TakeDamage(damage, attackNormal);
    // }
    // private void OnTriggerEnter(Collider collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {   

    //         print("Colisionando con jugador");
    //         Vector3 colPoint = collision.ClosestPoint(transform.position);
    //         attackNormal = transform.position - colPoint;

    //         Attack(collision.gameObject.GetComponent<IDamageable>());


    //         return;
    //     }
    //     print("Colisionando con algo");
    // }

    // private void OnTriggerExit(Collider col){

    //     if(col.gameObject.GetComponent<HealthManager>() != null){

    //         col.gameObject.GetComponent<HealthManager>().SetAttackStatus(false);

    //     }

    // }

    private void OnCollisionEnter(Collision col){

        ImPlayer player = col.gameObject.GetComponent<ImPlayer>();
        
        if(player != null && player.GetInvulnStatus()){

            Debug.Log("PlCl");
            Vector3 colPoint = col.GetContact(0).normal;
            //attackNormal = transform.position - colPoint;
            //attackNormal = -attackNormal;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            player.SetExternalSpeed(new Vector3(-colPoint.x * pushFactor * 1/2, -colPoint.y * pushFactor, 0));

            if(player.GetHealth() > 0){
                
                StartCoroutine(DamageBlinker(player));

            }

        }

    }
  

    private IEnumerator DamageBlinker(ImPlayer player){

        player.SetHealth(player.GetHealth() - damage);
        GameObject skin = player.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        GameObject joints = player.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;

        SkinnedMeshRenderer skinMr = skin.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer jointsMr = joints.GetComponent<SkinnedMeshRenderer>();
        player.gameObject.GetComponent<Rigidbody>().useGravity = false;
        player.SetDamageStatus(true);
        player.SetInvulnStatus(false);

        for(int count = 0; count < numberOfFlashes; count++){

            skinMr.enabled = false;
            jointsMr.enabled = false;
            
            yield return new WaitForSeconds(0.1f);

            skinMr.enabled = true;
            jointsMr.enabled = true;

            yield return new WaitForSeconds(0.1f);
            

        }
        player.gameObject.GetComponent<Rigidbody>().useGravity = true;
        player.SetDamageStatus(false);
        player.SetInvulnStatus(true);

    }

}
