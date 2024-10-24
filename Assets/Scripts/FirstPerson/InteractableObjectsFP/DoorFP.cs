using System.Collections;
using UnityEngine;

public class DoorFP : InteractableObject
{
    [SerializeField] private string closingDoorSoundName;
    [SerializeField] private float animationTime = 0.5f;
    protected GameObject player;
    private const float doorRotation = 90f;
    private bool isOpen = false;
    private Collider doorCollision;
    private GameObject pivot;
    private GameObject grandParent;

    private int initialRotation;

    protected override void _Awake()
    {
        doorCollision = GetComponent<Collider>();
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");  // Asigna el objeto con el tag "Player"
        }

        if (player == null)
        {
            Debug.LogError("Player no está asignado y no se encontró en la escena.");
        }
    }

    protected override void _Start()
    {
        pivot = transform.parent.gameObject;
        grandParent = pivot.transform.parent.gameObject;
    }

    protected override void _Update(){}

    protected override void _FixedUpdate(){}

    public override void Interact()
    {
        base.Interact();
        doorCollision.enabled = false;
        if (!isOpen)
        {
            int playerDirection = CheckIfInFront();
            isOpen = true;
            LeanTween.rotateY(pivot, initialRotation + (doorRotation * -playerDirection), animationTime).setEase(LeanTweenType.easeOutExpo).setOnComplete(returnCollision);
        }
        else
        {
            isOpen = false;
            LeanTween.rotateY(pivot, initialRotation, animationTime).setEase(LeanTweenType.easeOutExpo).setOnComplete(returnCollision);
        }
        
    }

    private void returnCollision()
    {
        doorCollision.enabled = true;
    }

    private int CheckIfInFront()
    {

        initialRotation = Mathf.RoundToInt(grandParent.transform.eulerAngles.y);
        Vector3 direction = player.transform.position - transform.position;

        switch (initialRotation)
        {
            case 0: return direction.z > 0 ? 1 : -1;
            case 90: return direction.x > 0 ? 1 : -1;
            case 180: return direction.z < 0 ? 1 : -1;
            case 270: return direction.x < 0 ? 1 : -1;
            default: return -1;
        }
    }
}