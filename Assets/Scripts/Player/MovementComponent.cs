using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float raycastSize = 1.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    public bool canMove = true;
    public bool canTakeInputs = true;

    [HideInInspector] public float moveInput;
    [HideInInspector] public bool onAir { get; private set; }

    private bool jumpRequest = false;
    public Vector3 playerMovement;
    public Vector3 externalVelocity = Vector3.zero;
    private Coroutine coyoteCoroutine;
    public bool onMobilePlatform = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        if (coyoteCoroutine == null && !onAir && !IsGrounded()) StartCoyoteCoroutine();
        if (coyoteCoroutine != null && onAir) CancelCoyoteCoroutine();
        if (onAir && IsGrounded()) onAir = false;
        if (!onMobilePlatform) rb.velocity = new Vector3(0, rb.velocity.y, 0);
        else rb.velocity = Vector3.zero;
        if (canTakeInputs) Move();
        Jump();
        rb.MovePosition(rb.position + (playerMovement + externalVelocity) * Time.fixedDeltaTime);
    }

    private void Move()
    {
        playerMovement = new Vector3(moveInput * movementSpeed, 0, 0);
    }

    private void Jump()
    {
        if (jumpRequest)
        {
            onMobilePlatform = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastSize);
    }

    private IEnumerator CoyoteTimeProcess()
    {
        yield return new WaitForSeconds(coyoteTime);
        onAir = true;
    }

    private void CancelCoyoteCoroutine()
    {
        StopCoroutine(coyoteCoroutine);
        coyoteCoroutine = null;
    }

    private void StartCoyoteCoroutine()
    {
        coyoteCoroutine = StartCoroutine(CoyoteTimeProcess());
    }
}
