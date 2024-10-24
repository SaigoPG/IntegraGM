using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] float mouseSensitivity = 20f;
    public void YRotation(float xMouse)
    {
        float mouseX = mouseSensitivity * xMouse;
        transform.Rotate(Vector3.up * mouseX);
    }
    public void XRotation(float yMouse)
    {
        float mouseY = -mouseSensitivity * yMouse;
        Vector3 currentRotation = head.transform.localEulerAngles;
        if (currentRotation.x > 180f) currentRotation.x -= 360f;
        currentRotation.x += mouseY;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -70f, 70f);
        head.transform.localEulerAngles = currentRotation;
    }
}
