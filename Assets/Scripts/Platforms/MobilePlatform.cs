using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float platformSpeed;
    [SerializeField] private float cooldownTime;

    private bool moving = true;
    private Rigidbody rb;
    private MovementComponent playerMovement;
    private Vector3 currPlatformPos, prevPlatformPos, velocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CalcVelocity();
        MovePlatform();
    }
    void CalcVelocity()
    {

        velocity = (currPlatformPos - prevPlatformPos) / Time.fixedDeltaTime;
        prevPlatformPos = currPlatformPos;
        currPlatformPos = gameObject.transform.position;

    }
    private void MovePlatform()
    {
        if (!moving) return;
        rb.MovePosition(Vector3.MoveTowards(rb.position, endPoint.position, platformSpeed * Time.deltaTime));

        if (Vector3.Distance(rb.position, endPoint.position) <= 0)
        {
            Transform tmp = startPoint;
            startPoint = endPoint;
            endPoint = tmp;
            StartCoroutine(WaitForMove());
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        { 
            playerMovement = col.GetComponent<MovementComponent>();
            if (velocity.y > 0) playerMovement.GetComponent<Rigidbody>().useGravity = false;
            else playerMovement.GetComponent<Rigidbody>().useGravity = true;
            playerMovement.externalVelocity = velocity;
            playerMovement.onMobilePlatform = true;
            PlayerAnimator playerAnimator = col.GetComponent<PlayerAnimator>();
            if (playerAnimator != null)
            {
                playerAnimator.isOnMovingPlatform = true;
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (velocity.y > 0) playerMovement.GetComponent<Rigidbody>().useGravity = false;
            else playerMovement.GetComponent<Rigidbody>().useGravity = true;
            playerMovement.externalVelocity = velocity;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        col.transform.SetParent(null);
        if (col.CompareTag("Player"))
        {
            playerMovement.GetComponent<Rigidbody>().useGravity = true;
            playerMovement.externalVelocity = Vector3.zero;
            playerMovement.onMobilePlatform = false;
            playerMovement = null;
            PlayerAnimator playerAnimator = col.GetComponent<PlayerAnimator>();
            if (playerAnimator != null)
            {
                playerAnimator.isOnMovingPlatform = false;
            }
        }
    }


    IEnumerator WaitForMove()
    {
        moving = false;
        yield return new WaitForSeconds(cooldownTime);
        moving = true;
    }
}
