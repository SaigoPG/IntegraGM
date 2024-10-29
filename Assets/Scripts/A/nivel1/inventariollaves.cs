using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventariollaves : MonoBehaviour
{
    public List<string> keys = new List<string>();  // Lista para guardar las llaves recogidas

    public void PickUpKey(string keyName)
    {
        if (!keys.Contains(keyName)) 
        {
            keys.Add(keyName);
            Debug.Log("Has recogido: " + keyName);
        }
        else
        {
            Debug.Log("Ya tienes la llave: " + keyName);
        }
    }

    //Verificar si el jugador tiene una llave
    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    public void UseKey(string keyName)
    {
        if (keys.Contains(keyName))
        {
            keys.Remove(keyName);  // Remover la llave después de usarla
            Debug.Log("Has usado la llave: " + keyName);
        }
        else
        {
            Debug.Log("No tienes la llave: " + keyName);
        }
    }

}
