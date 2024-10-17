using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private Collider doorCollider;
    [SerializeField] private bool isLocked;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject unlockedText;

    private const float doorRotation = 80;
    private int playerDirection;
    private Coroutine currentCorroutine;

    private ImPlayer plRef;

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);   

        plRef = collision.gameObject.GetComponent<ImPlayer>();

        if (collision.gameObject.CompareTag("Player") && active)
        {
            if(isLocked && !plRef.GetDoorKey()){lockedText.SetActive(true);}
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
        plRef = null;
        lockedText.SetActive(false);
        unlockedText.SetActive(false);
    }

    public override void Interact()
    {      
        if((!isLocked) || (isLocked && plRef.GetDoorKey())){
            StopInteraction();
            StartCoroutine(OpeningDoor());
        } else {

            lockedText.SetActive(true);

        }
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
