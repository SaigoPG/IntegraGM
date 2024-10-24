using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementComponent : MonoBehaviour
{
    private CharacterController characterController;

    public float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] public float gravity = 100f;
    [SerializeField] private float raycastSize = 1.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    public bool canMove = true;
    public bool canTakeInputs = true;

    [HideInInspector] public float moveInput;
    [HideInInspector] public bool onAir {  get; private set; }

    private bool jumpRequest = false;
    public Vector3 playerMovement;
    public float fallVelocity { get; private set; } = 0;

    Coroutine coyoteCoroutine;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!canMove) return;
        if (coyoteCoroutine == null && !onAir && !characterController.isGrounded) StartCoyoteCoroutine();
        if (coyoteCoroutine != null && onAir) CancelCoyoteCoroutine();
        if (onAir && characterController.isGrounded) onAir = false;

        SetGavity();
        Jump();
        HandleHeadCollisions();
        if (canTakeInputs) Move();
        playerMovement.y = fallVelocity;
        characterController.Move(playerMovement * Time.deltaTime);
    }

    private void Move()
    {
        playerMovement.x = moveInput * movementSpeed;
    }
    private void SetGavity()
    {
        if (!onAir)
        {
            fallVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            fallVelocity += -gravity * Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (jumpRequest)
        {
            fallVelocity = jumpForce;
            jumpRequest = false;
            onAir = true;
        }
    }

    public void SetMovementDirection(float movementDirection)
    {
        moveInput = movementDirection;
    }

    public void SetJumpRequest(bool onFloorJump)
    {
        if (!canTakeInputs) return;
        if (!onAir && onFloorJump) jumpRequest = true;
        else if (onAir && !onFloorJump) jumpRequest = true;
    }

    private void HandleHeadCollisions()
    {
        Ray ray = new Ray(transform.position, Vector3.up * raycastSize);
        Debug.DrawRay(ray.origin, ray.direction * raycastSize);
        RaycastHit[] hits = Physics.RaycastAll(ray, raycastSize);
        if (hits.Length == 0) return;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                fallVelocity = -gravity * Time.fixedDeltaTime;
                break;
            }
        }
    }

    IEnumerator CoyoteTimeProcess()
    {
        yield return new WaitForSeconds(coyoteTime);
        onAir = true;
    }

    void CancelCoyoteCoroutine()
    {
            StopCoroutine(coyoteCoroutine);
            coyoteCoroutine = null;
    }

    void StartCoyoteCoroutine()
    {
        coyoteCoroutine = StartCoroutine(CoyoteTimeProcess());
    }
}
