using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dooranimation : MonoBehaviour
{
    public Vector3 openRotation;  
    public Vector3 closedRotation; 
    public float openSpeed = 2f; 
    private bool isOpen = false;  

    void Start()
    {
        transform.localEulerAngles = closedRotation;
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Abriendo puerta con transform");
            StopAllCoroutines();
            StartCoroutine(RotateDoor(openRotation));
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            Debug.Log("Cerrando puerta con transform");
            StopAllCoroutines();
            StartCoroutine(RotateDoor(closedRotation));
            isOpen = false;
        }
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {

        while (Vector3.Distance(transform.localEulerAngles, targetRotation) > 0.01f)
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.localEulerAngles = targetRotation;
    }
}
