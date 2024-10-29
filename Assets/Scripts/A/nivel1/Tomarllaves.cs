using UnityEngine;

public class Tomarllaves : MonoBehaviour
{
    public string keyName;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventariollaves playerInventory = other.GetComponent<inventariollaves>();
            if (playerInventory != null)
            {
                playerInventory.PickUpKey(keyName); 
                Destroy(gameObject);

                FindObjectOfType<pistas>().JugadorInteraccion();
            }
        }
    }
}
