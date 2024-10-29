using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class DestructiblePlatform : MonoBehaviour
{
    [SerializeField] private float DestroyTime = 4f;
    [SerializeField] private float RestartTime = 5f;
    [SerializeField] private Color[] colors; // Array de colores
    
    private Renderer renderer;
    private Collider collider;

    bool isInDestroyChange = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        ChangeColor(0);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        if (isInDestroyChange) return;
        isInDestroyChange = true;
        StartCoroutine(DestroyProcess());

    }

    private void ChangeColor(int index)
    {
        // Cambiar el color del material basado en el índice del array
        if (index >= 0 && index < colors.Length && renderer != null)
        {
            renderer.material.color = colors[index];
        }
    }

    IEnumerator DestroyProcess()
    {
        float currentTime = 0;
        ChangeColor(1);
        yield return new WaitForSeconds(DestroyTime/3);
        currentTime += DestroyTime/3;
        ChangeColor(2);
        yield return new WaitForSeconds(DestroyTime / 3);
        currentTime += DestroyTime / 6;
        ChangeColor(3);
        while (currentTime < DestroyTime)
        {
            ChangeColor(4);
            yield return new WaitForSeconds(DestroyTime / 24);
            ChangeColor(5);
            yield return new WaitForSeconds(DestroyTime / 24);
            currentTime += DestroyTime / 12;
        }
        collider.enabled = false;
        renderer.enabled = false;
        StartCoroutine(RestartProcess());
    }

    IEnumerator RestartProcess()
    {
        yield return new WaitForSeconds(RestartTime);
        isInDestroyChange = false;
        collider.enabled = true;
        renderer.enabled = true;
        ChangeColor(0);
    }
}
