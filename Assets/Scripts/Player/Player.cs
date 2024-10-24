using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(InteractionComponent))]
[RequireComponent(typeof(InventoryComponent))]
[RequireComponent (typeof(MoveWithFloor))]
public class Player : MonoBehaviour
{
    private MovementComponent movementComponent;
    private InteractionComponent interactionComponent;
    private InventoryComponent inventoryComponent;
    private PlayerInput playerInput;
    private void Awake()
    {
        inventoryComponent = GetComponent<InventoryComponent>();
        interactionComponent = GetComponent<InteractionComponent>();
        movementComponent = GetComponent<MovementComponent>();
        playerInput = GetComponent<PlayerInput>();
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
        movementComponent.SetMovementDirection(context.ReadValue<float>());
    }

    public void JumpActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!movementComponent.onAir) movementComponent.SetJumpRequest(true);
        else
        {
            GraspableObject graspableObject = inventoryComponent.FindObject("AirJump", true);
            if (graspableObject == null) return;
            PowerUpBase airJumpPowerUp = graspableObject.GetComponent<PowerUpBase>();
            airJumpPowerUp.ActivePowerUp();
        }
    }

    public void PauseActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (GameManager.Instance.pause) GameManager.Instance.ChangePauseMode(false);
        else GameManager.Instance.ChangePauseMode(true);
    }

    public void InteractActionEmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        interactionComponent.ExecuteInteraction();
    }

    public void DashActionEmit(InputAction.CallbackContext context)
    {
        GraspableObject graspableObject = inventoryComponent.FindObject("Dash", true);
        if (graspableObject == null) return;
        PowerUpBase dashPowerUp = graspableObject.gameObject.GetComponent<PowerUpBase>();
        dashPowerUp.ActivePowerUp();
    }

    public void ControlChanged(PlayerInput playerInput)
    {
        Debug.Log("Cambio de dispositivo: " + playerInput.currentControlScheme);
    }
}
