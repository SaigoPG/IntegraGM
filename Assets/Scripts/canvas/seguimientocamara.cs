using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguimientocamara : MonoBehaviour
{
    public GameObject target;  

    private Vector3 offset;    
    private Vector3 Posinicial; 

    public float derechaMax;    
    public float izqMax;        
    public float alturaMax;     
    public float alturaMin;     

    public float speed;        
    public bool encendido = true;

    private void Awake()
    {
        Posinicial = transform.position;

        if (target)
        {
            offset = Posinicial - target.transform.position;
        }
    }

    void Movecam()
    {
        if (target)
        {
            Vector3 targetPosition = target.transform.position + offset;

            targetPosition.z = Mathf.Clamp(targetPosition.z, derechaMax, izqMax);
            targetPosition.x = Mathf.Clamp(targetPosition.x, alturaMin, alturaMax);

            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (encendido)
        {
            Movecam(); 
        }
    }
}
