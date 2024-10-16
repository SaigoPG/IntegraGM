using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string currentKey = "";  

    public void PickUpKey(string keyName)
    {
        currentKey = keyName;
        Debug.Log("Has recogido: " + keyName);
    }

    public void UseKey()
    {
        Debug.Log("Has usado la llave: " + currentKey);
        currentKey = ""; 
    }
}
