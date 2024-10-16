using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.PickUpKey(keyName); 
                Destroy(gameObject);  
            }
        }
    }
}
