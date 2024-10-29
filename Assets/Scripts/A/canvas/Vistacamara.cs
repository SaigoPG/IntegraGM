using UnityEngine;

public class Vistacamara: MonoBehaviour
{
    public float speed;
    public Vector3 offset;
    private Vector3 posZ;
    private Vector3 posX;

    public void UpdateCameraPosition(Vector3 playerPosition)
    {
        Vector3 targetPosition = playerPosition + offset;

        // Aquí podría haber restricciones de movimiento como antes
        targetPosition.z = Mathf.Clamp(targetPosition.z, -10, 10); // Ejemplo
        targetPosition.x = Mathf.Clamp(targetPosition.x, -5, 5);   // Ejemplo

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
