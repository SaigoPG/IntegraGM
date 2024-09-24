using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpKey : InteractableObject
{
    private Player player;
    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.CompareTag("Player") && active)
        {
            player = collision.gameObject.GetComponent<Player>();
        }
    }

    public override void Interact()
    {
        player.doubleJump = true;
        Destroy(gameObject);
    }
}
