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
    private Vector3 currentPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (!moving) return;
        rb.MovePosition(Vector3.MoveTowards(rb.position, endPoint.position,platformSpeed*Time.deltaTime));
        if(Vector3.Distance(rb.position,endPoint.position) <= 0)
        {
            Transform tmp = startPoint;
            startPoint = endPoint;
            endPoint = tmp;
            StartCoroutine(WaitForMove());
        }
    }

    IEnumerator WaitForMove()
    {
        moving = false;
        yield return new WaitForSeconds(cooldownTime);
        moving = true;
    }
}
