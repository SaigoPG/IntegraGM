using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFP : MonoBehaviour
{
    [SerializeField] private RaycastInteraction raycastInteraction;

    private MovementComponentFP movementComponent;
    private CameraRotation cameraRotation;
    private PlayerInput playerInput;   
    private void Awake()
    {
        Transform body = transform.Find("Body");
        movementComponent = GetComponent<MovementComponentFP>();
        cameraRotation = body.GetComponent<CameraRotation>();
        playerInput = GetComponent<PlayerInput>();
        movementComponent.body = body;
    }

    private void OnEnable()
    {
        playerInput.enabled = true;
    }

    private void OnDisable()
    {
        playerInput.enabled = false;
    }

    public void MoveActionEmit(InputAction.CallbackContext context)
    {
        movementComponent.moveInput = context.ReadValue<Vector2>();
    }

    public void JumpActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        movementComponent.SetJumpRequest();
    }

    public void InteractActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        raycastInteraction.ExecuteInteraction();
    }

    public void PauseActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (GameManager.Instance.pause)
        {
            GameManager.Instance.ChangePauseMode(false);
            GameManager.Instance.ChangeMouseMode(true);
        }
        else
        {
            GameManager.Instance.ChangePauseMode(true);
            GameManager.Instance.ChangeMouseMode(false);
        }
    }

    public void cameraActionEmit(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        float mouseX = mouseDelta.x * Time.deltaTime;
        float mouseY = mouseDelta.y * Time.deltaTime;
        cameraRotation.YRotation(mouseX);
        cameraRotation.XRotation(mouseY);
    }

    public void ControlChanged(PlayerInput playerInput)
    {
        Debug.Log("Cambio de dispositivo: " + playerInput.currentControlScheme);
    }
}
