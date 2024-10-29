using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertasllaves : MonoBehaviour
{
    public bool isLocked = true;          
    public string requiredKey;            
    public GameObject texto;              
    public GameObject textoNoKey;         
    private bool playerInRange;           
    private bool doorUnlocked = false;    
    private inventariollaves playerInventory; 
    public Dooranimation doorAnimationScript; 

    private void Start()
    {
        texto.SetActive(false);           
        textoNoKey.SetActive(false);      

        if (doorAnimationScript == null)
        {
            Debug.LogError("El script DoorAnimation no está asignado en puertasllaves.");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (doorUnlocked)
            {
                //script de la puerta para abrirla
                doorAnimationScript.OpenDoor();
            }
            else
            {
                TryUnlock();
            }
        }
    }

    public void TryUnlock()
    {
        if (isLocked && playerInventory != null && playerInventory.HasKey(requiredKey))
        {
            UnlockDoor();  
            playerInventory.UseKey(requiredKey);
            FindObjectOfType<pistas>().JugadorInteraccion();
        }
        else
        {
            texto.SetActive(false);  
            textoNoKey.SetActive(true); 
            StartCoroutine(HideNoKeyMessage()); 
            FindObjectOfType<pistas>().JugadorInteraccion();
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        doorUnlocked = true;
        if (playerInRange)
        {
            texto.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<inventariollaves>();  
            playerInRange = true;

            if (!doorUnlocked)
            {
                texto.SetActive(true);  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.SetActive(false);  
            textoNoKey.SetActive(false);  
            playerInRange = false;
            
            if (doorUnlocked)
            {
                doorAnimationScript.CloseDoor();
            }
        }
    }

    private IEnumerator HideNoKeyMessage()
    {
        yield return new WaitForSeconds(2);  // Esperar 2 segundos
        textoNoKey.SetActive(false);         // Ocultar el mensaje
    }
}
