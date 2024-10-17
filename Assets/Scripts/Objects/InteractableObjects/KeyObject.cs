using System.Runtime.CompilerServices;
using UnityEngine;

enum AbilitiesGranted{

    DoubleJump,
    Dash,
    DoorKey

}

public class KeyObject : InteractableObject{

    [SerializeField] private AbilitiesGranted abilities;

    private ImPlayer player;

    protected override void OnTriggerEnter(Collider col){

        base.OnTriggerEnter(col);

        if(col.gameObject.CompareTag("Player") && active){

            player = col.gameObject.GetComponent<ImPlayer>();

        }
    }

    public override void Interact(){        

        switch(abilities){

            case AbilitiesGranted.DoubleJump:

                player.SetJumpKey(true);
                StopInteraction();
                Destroy(gameObject);

                break;

            case AbilitiesGranted.Dash:

                player.SetDashKey(true);
                StopInteraction();
                Destroy(gameObject);

                break;

            case AbilitiesGranted.DoorKey:

                player.SetDoorKey(true);
                StopInteraction();
                Destroy(gameObject);

                break;
            
            default:

                Debug.Log("InteractableErr");

                break;

        }

    }
}
