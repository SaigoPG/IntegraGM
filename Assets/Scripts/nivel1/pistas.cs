using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pistas : MonoBehaviour
{
    public GameObject dialogPanel;      
    public TextMeshProUGUI dialogText;  
    public string[] hints;              
    public float tiempoInactividad = 10f;  
    public float tiempoPistaVisible = 3f;  

    private int currentHintIndex = 0;   
    private float tiempoSinInteraccion; 
    private bool isHintVisible = false; 
    private bool canCloseHint = false;  

    void Start()
    {
        if (hints.Length == 0)
            Debug.LogWarning("No se han configurado pistas en el HintManager.");

        dialogPanel.SetActive(false);    
        tiempoSinInteraccion = 0f;       
    }

    void Update()
    {
        if (!isHintVisible)
        {
            tiempoSinInteraccion += Time.deltaTime; 


            if (tiempoSinInteraccion >= tiempoInactividad)
            {
                MostrarPista(); 
                tiempoSinInteraccion = 0f; 
            }
        }

        if (canCloseHint && Input.anyKeyDown && dialogPanel.activeSelf)
        {
            dialogPanel.SetActive(false);
            isHintVisible = false; 
        }
    }

    public void JugadorInteraccion()
    {
        tiempoSinInteraccion = 0f; 
    }

    public void MostrarPista()
    {
        if (!dialogPanel.activeSelf && currentHintIndex < hints.Length)
        {
            dialogText.text = hints[currentHintIndex];  
            dialogPanel.SetActive(true);                
            isHintVisible = true;                       
            canCloseHint = false;                       
            currentHintIndex++;                         
            StartCoroutine(MinimumHintVisibility());    
        }
        else if (currentHintIndex >= hints.Length)
        {
            currentHintIndex = 0;  
        }
    }

    private IEnumerator MinimumHintVisibility()
    {
        yield return new WaitForSeconds(tiempoPistaVisible); // Esperar el tiempo mínimo
        canCloseHint = true;  // Ahora el jugador puede cerrar la pista
    }
}
