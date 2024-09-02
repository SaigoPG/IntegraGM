using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class charMovement : MonoBehaviour
{
    private Rigidbody rb;
    private InputSystem input;

    private InputAction movementAction;
    private InputAction jumpAction;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private bool isGrounded;
    private bool isDoubleJumpSpent;

    private void Awake(){

        rb = GetComponent<Rigidbody>();
        input = new InputSystem();
        movementSpeed = 10f;
        jumpForce = 5f;

    }

    private void OnEnable(){

        movementAction = input.Land.Movement;
        movementAction.Enable();

        input.Land.Jump.performed += OnJump;
        input.Land.Jump.Enable();           

    }

    private void OnDisable(){

        input.Land.Movement.Disable();
        input.Land.Movement.Disable();

    }

    private void OnJump(InputAction.CallbackContext ctx){

        if(isGrounded){

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");

        }

        if(!isGrounded && !isDoubleJumpSpent){

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isDoubleJumpSpent = true;
            Debug.Log("DoubleJump");            

        }

    }

    private void OnCollisionEnter(Collision col){

        if(col.gameObject.CompareTag("Ground")){

            isGrounded = true;
            isDoubleJumpSpent = false;
            Debug.Log("grounded");

        }

    }

    private void OnCollisionExit(Collision col){

        isGrounded = false;        
        Debug.Log("!grounded");

    }

    private void FixedUpdate(){

        float moveDir = movementAction.ReadValue<float>();
        Debug.Log($"Dir {moveDir}");
        Vector3 velocity = rb.velocity;
        velocity.x = movementSpeed * moveDir;
        rb.velocity = velocity;      

    }

}
