using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class ImPlayer : MonoBehaviour
{

    [Header("Player stats")]
    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float jumpHeight;

    [Header("Misc stuff")]
    [SerializeField] private float groundThreshold;
    [SerializeField] private float wallThreshold;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isAgainstWall;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool isOnPlatform;
    [SerializeField] private bool isDoubleJumpSpent;
    [SerializeField] private bool isGettingHurt;
    [SerializeField] private bool canTakeDamage;
    [SerializeField] private Vector3 externalSpeed;

    [Header("Key Objects Checks")]
    [SerializeField] private bool hasDoubleJump;
    [SerializeField] private bool hasDash;
    [SerializeField] private bool hasDoorKey;

    //Refs

    private Rigidbody physChar;
    private PlayerAnimator charAnimator;
    private PlayerControls pc;
    private float moveDir;

    //Utility Setters/Getters

    public void SetJumpKey(bool jumpKeyStatus){

        hasDoubleJump = jumpKeyStatus;

    }

    public void SetDashKey(bool dashKeyStatus){

        hasDash = dashKeyStatus;

    }

    public void SetDoorKey(bool doorKeyStatus){

        hasDoorKey = doorKeyStatus;

    }

    public bool GetDoorKey(){

        return hasDoorKey;

    }

    public void SetDamageStatus(bool damageStatus){

        isGettingHurt = damageStatus;

    }

    public bool GetInvulnStatus(){

        return canTakeDamage;

    }

    public void SetInvulnStatus(bool incomingStatus){

        canTakeDamage = incomingStatus;

    }

    public float GetHealth(){

        return health;

    }

    public void SetHealth(float incomingHealth){

        health = incomingHealth;

    }

    public void SetExternalSpeed(Vector3 incomingPush){

        externalSpeed = incomingPush;

    }

    ///Own funcs

    private void Movement(){

        if(!isAgainstWall){

            Vector3 absSpeed = new Vector3(moveDir * movementSpeed, 0, 0);            

            if(isOnPlatform){

                absSpeed += externalSpeed;

            }

            if(isGettingHurt){
                
                absSpeed += externalSpeed;

            }

            
            absSpeed *= Time.fixedDeltaTime;

            physChar.MovePosition(physChar.position + absSpeed);
        }
    }

    //Unity Events

    private void OnTriggerEnter(Collider col){

        MovingPlatform mp = col.GetComponent<MovingPlatform>();

        if(mp != null){

            isOnPlatform = true;
            externalSpeed = mp.GetVelocity();

        }

    }

    private void OnTriggerStay(Collider col){

        MovingPlatform mp = col.GetComponent<MovingPlatform>();

        if(isOnPlatform && mp != null){

            externalSpeed = mp.GetVelocity();

        }

        if(!isOnPlatform && mp != null){

            isOnPlatform = true;
            externalSpeed = mp.GetVelocity();

        }

    }

    private void OnTriggerExit(){

        Debug.Log("TE");
        isOnPlatform = false;
        externalSpeed = Vector3.zero;

    }    

    //Own events
    private void OnMove(InputValue dir){

        moveDir = dir.Get<float>();
        
    }

    private void OnJump(){

        Debug.Log("Jump");
        
        if(isGrounded || !isDoubleJumpSpent && hasDoubleJump){
            isJumping = true;
            physChar.velocity = new Vector3(physChar.velocity.x,0,0);
            physChar.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            if(!isGrounded){isDoubleJumpSpent = true;}

        }


    }

    private void OnDash(){

        Debug.Log("Dash");
        if(hasDash && !isDashing){

            isDashing = true;
            StartCoroutine(Dash());

        }

    }

    private void CheckGrounded(){

        Vector3 offFront = new Vector3(0.5f, 0, 0);
        Vector3 offBack = new Vector3(-0.5f, 0, 0); 

        int layerMask = 1 << 2;
        layerMask = ~layerMask;  
    
        if(Physics.Raycast(transform.position, Vector3.down, groundThreshold, layerMask) || Physics.Raycast(transform.position + offFront/2, Vector3.down, groundThreshold, layerMask) ||Physics.Raycast(transform.position + offFront, Vector3.down, groundThreshold, layerMask) || Physics.Raycast(transform.position + offBack/2, Vector3.down, groundThreshold, layerMask) || Physics.Raycast(transform.position + offBack, Vector3.down, groundThreshold, layerMask)){

            isGrounded = true;
            isDoubleJumpSpent = false;

        } else {

            isGrounded = false;

        }

    }

    private void CheckWallCollisions(){
        
        Ray ray = new Ray(transform.position, new Vector3(moveDir,0,0));

        if (Physics.Raycast(ray, out RaycastHit hit, wallThreshold))
        {
            if (!hit.collider.gameObject.CompareTag("Platform") && !hit.collider.isTrigger)
            {
                isAgainstWall = true;
            }

        }
        else
        {

            isAgainstWall = false;

        }

    }

    private void Awake(){

        pc = new PlayerControls();
        physChar = GetComponent<Rigidbody>();
        charAnimator = GetComponent<PlayerAnimator>();
        canTakeDamage = true;
        isGettingHurt = false;

    }

    private void Update(){

        CheckGrounded();
        CheckWallCollisions();
        
        charAnimator.UpdateAnimations(moveDir, physChar.velocity.y, isGrounded);

    }

    private void FixedUpdate(){
        
        Movement();       

    } 
    
    private IEnumerator Dash(){

        hasDash = false;
        float localJump;
        if(isJumping){localJump = physChar.velocity.y;} else {localJump = 0;}
        physChar.velocity = new Vector3(moveDir * dashSpeed, localJump,0);
        yield return new WaitForSeconds(0.02f);
        isDashing = false;
        yield return new WaitForSeconds(2);
        hasDash = true;

    }
    
}
