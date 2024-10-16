using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palancapuerta : MonoBehaviour
{
    public GameObject door; 
    public float doorOpenHeight = 5f; 
    public float openSpeed = 2f; 
    private bool isPlayerNear = false; 
    private bool isDoorOpen = false;
    public GameObject texto;

    private Vector3 initialDoorPosition; 
    private Vector3 targetDoorPosition;

    void Start()
    {
        initialDoorPosition = door.transform.position;
        targetDoorPosition = new Vector3(initialDoorPosition.x, initialDoorPosition.y + doorOpenHeight, initialDoorPosition.z);
        texto.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isDoorOpen)
        {
            StartCoroutine(LiftDoor());
        }
    }

    IEnumerator LiftDoor()
    {
        isDoorOpen = true;

        while (Vector3.Distance(door.transform.position, targetDoorPosition) > 0.01f)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, targetDoorPosition, Time.deltaTime * openSpeed);
            yield return null;
        }

        door.transform.position = targetDoorPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }

        texto.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }

        texto.SetActive(false);
    }

}