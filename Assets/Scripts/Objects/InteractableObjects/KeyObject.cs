using UnityEngine;

public class KeyObject : InteractableObject
{    

    [SerializeField] bool grantsDoubleJump;
    [SerializeField] bool grantsDash;

    private ImPlayer player;

    protected override void OnTriggerEnter(Collider col){

        base.OnTriggerEnter(col);

        if(col.gameObject.CompareTag("Player") && active){

            player = col.gameObject.GetComponent<ImPlayer>();

        }
    }

    public override void Interact(){
        if(grantsDoubleJump){

            player.SetJumpKey(true);
            StopInteraction();
            Destroy(gameObject);

        }

        if(grantsDash){

            player.SetDashKey(true);
            StopInteraction();
            Destroy(gameObject);

        }       

    }
}
