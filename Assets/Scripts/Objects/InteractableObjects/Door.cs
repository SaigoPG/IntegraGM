using System.Collections;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private string ClosingDoorSoundName;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private Collider doorCollider;
    private const float doorRotation = 80;
    private int playerDirection;
    private Coroutine currentCorroutine;

    protected override void _Awake(){}

    protected override void _Start(){}

    protected override void _Update() { }

    protected override void _FixedUpdate() { }

    protected void OnTriggerEnter(Collider collision)
    {
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

    protected void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            currentCorroutine = StartCoroutine(ClosingDoor());
        }
    }

    public override void Interact()
    {
        base.Interact();
        active = false;
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
        yield return new WaitForSeconds(3f);
        LeanTween.rotateY(gameObject, 0, animationTime).setEaseOutExpo();
        doorCollider.enabled = true;
        active = true;
        soundEmitter.emitSound(ClosingDoorSoundName);
    }
}
