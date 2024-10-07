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

    private Animator animator;            

    private void Start()
    {
        animator = GetComponent<Animator>();
        texto.SetActive(false);           
        textoNoKey.SetActive(false);      
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (doorUnlocked)
            {
                // Abrir la puerta si ya está desbloqueda
                OpenDoor();
            }
            else
            {
                // Intentar desbloquear la puerta si está bloqueada
                TryUnlock();
            }
        }
    }

    public void TryUnlock()
    {
        // Verificar si la puerta está bloqueada y si el jugador tiene la llave correcta
        if (isLocked && playerInventory != null && playerInventory.HasKey(requiredKey))
        {
            UnlockDoor();  // Desbloquear la puerta
            playerInventory.UseKey(requiredKey);

            FindObjectOfType<pistas>().JugadorInteraccion();
        }
        else
        {
            texto.SetActive(false);  
            textoNoKey.SetActive(true); 
            StartCoroutine(HideNoKeyMessage());  // Ocultar mensaje después de unos segundos

            FindObjectOfType<pistas>().JugadorInteraccion();
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        doorUnlocked = true;
        Debug.Log("La puerta ha sido desbloqueada.");
        if (playerInRange)
        {
            texto.SetActive(true);
        }
    }

    private void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("OpenDoor");  // Activar la animación de apertura
        }
        texto.SetActive(false);  
        doorUnlocked = true;     
    }

    // Detectar cuando el jugador entra en el área del trigger
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
        }
    }

    private IEnumerator HideNoKeyMessage()
    {
        yield return new WaitForSeconds(2);  // Esperar 2 segundos
        textoNoKey.SetActive(false);         // Ocultar el mensaje
    }
}
