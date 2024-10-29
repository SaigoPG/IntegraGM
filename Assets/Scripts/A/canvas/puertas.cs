using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puertas : MonoBehaviour
{
    public bool isLocked = true;          // Define si la puerta está bloqueada
    public string requiredKey;            // Nombre del objeto necesario para desbloquear la puerta (por ejemplo, "LlaveRoja")
    public GameObject texto;              // El texto que indica "Presiona E para entrar"
    public int sceneToLoad;               // La escena que se cargará cuando la puerta esté desbloqueada
    private bool playerInRange;           // Variable para saber si el jugador está dentro del rango de interacción
    private bool doorUnlocked = false;    // Verifica si la puerta está desbloqueada
    private PlayerInventory playerInventory; // Referencia al inventario del jugador

    private Animator animator;            // Referencia al componente Animator para animar la puerta

    private void Start()
    {
        animator = GetComponent<Animator>();
        texto.SetActive(false);           // El mensaje de "Presiona E" debe estar oculto al inicio
    }

    private void Update()
    {
        // Verificar si el jugador está en el rango, la puerta está desbloqueada y se presiona la tecla E
        if (playerInRange && doorUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);  // Cambiar de escena
        }
    }

    // Método para intentar abrir la puerta
    public void TryUnlock()
    {
        // Si la puerta está bloqueada y el jugador tiene la llave correcta
        if (isLocked && playerInventory != null && playerInventory.currentKey == requiredKey)
        {
            UnlockDoor();  // Desbloquear la puerta
        }
        else
        {
            Debug.Log("Necesitas la llave correcta para abrir esta puerta.");
        }
    }

    // Método para desbloquear la puerta
    private void UnlockDoor()
    {
        isLocked = false;
        doorUnlocked = true;  // Marca que la puerta ha sido desbloqueada
        Debug.Log("La puerta ha sido desbloqueada.");

        if (animator != null)
        {
            animator.SetTrigger("OpenDoor");  // Reproducir animación de apertura si existe
        }

        if (playerInRange)
        {
            texto.SetActive(true);  // Si el jugador ya está en el rango, mostrar el texto
        }
    }

    // Detectar si el jugador entra en el área de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();  // Obtener el inventario del jugador

            // Intentar desbloquear la puerta
            TryUnlock();

            // Si la puerta ya está desbloqueada, mostrar el mensaje "Presiona E"
            if (!isLocked)
            {
                texto.SetActive(true);
                playerInRange = true;    // El jugador está dentro del rango
            }
        }
    }

    // Detectar si el jugador sale del área de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.SetActive(false);  // Ocultar el mensaje cuando el jugador salga del rango
            playerInRange = false;   // El jugador ya no está en el rango
        }
    }
}