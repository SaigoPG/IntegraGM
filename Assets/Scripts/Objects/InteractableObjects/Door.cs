using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private Collider doorCollider;

    private const float doorRotation = 80;
    private int playerDirection;
    private Coroutine currentCorroutine;

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.CompareTag("Player") && active)
        {
            playerDirection = ((collision.transform.position.x - transform.position.x) > 0) ? 1: -1 ;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(currentCorroutine);
            currentCorroutine = null;
        }
    }

    protected override void OnTriggerExit(Collider collision)
    {
        base.OnTriggerExit(collision);
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            currentCorroutine = StartCoroutine(ClosingDoor());
        }
    }

    public override void Interact()
    {
        
        StopInteraction();
        StartCoroutine(OpeningDoor());
    }

    private IEnumerator OpeningDoor()
    {
        active = false;
        LeanTween.rotateY(gameObject, doorRotation * playerDirection, animationTime).setEaseOutExpo();
        yield return new WaitForSeconds(animationTime / 2);
        doorCollider.enabled = false;
    }

    private IEnumerator ClosingDoor()
    {
        yield return new WaitForSeconds(4f);
        LeanTween.rotateY(gameObject, 0, animationTime).setEaseOutExpo();
        doorCollider.enabled = true;
        active = true;
    }
}
