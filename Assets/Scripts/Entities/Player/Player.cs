using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : DamageableEntity, IHealable
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastSize = 1f;
    [SerializeField] private int flashNumber;
    private MeshRenderer msr;

    private Color originalColor;

    private bool jumpRequest = false;
    private bool isDoubleJumpSpent = false;
    private bool isOnPlatform = false;
    private float moveInput;
    private Vector3 platformVelocity;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();        
        
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnControllerColliderHit(ControllerColliderHit hit){

        GameObject obj = hit.gameObject;
        

        if(obj.GetComponent<MovingPlatform>() != null){
                       
            MovingPlatform mp = obj.GetComponent<MovingPlatform>();
            
            if(!mp.getTrolling()){
                
                isOnPlatform = true;
                platformVelocity = mp.getVelocity();
                Debug.Log(platformVelocity); 

            }

        } else {

            isOnPlatform = false;

        }        

    }


    protected override void Movement()
    {
        SetGavity();
        Vector3 playerMovement = Vector3.zero;
        playerMovement.x = HandleWalk();        

        HandleJump();
        HandleHeadCollisions();
        playerMovement.y = fallVelocity;

        if(isOnPlatform){

            playerMovement += platformVelocity;

        }

        characterController.Move(playerMovement * Time.fixedDeltaTime);
    }

    public override void Death()
    {
        //Ejecutar animacion sonido o lo que sea que ocurra cuando muera
    }

    
    public override void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(damageFlash());
        
        if (health <= 0)
        {
            Death();
        }
        print(health);
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
    //Funciones del manejo del movimiento y salto

    private void HandleJump()
    {
        if (jumpRequest)
        {
            fallVelocity = jumpForce;
            jumpRequest = false;            
        }
    }
    
    private float HandleWalk()
    {   

        float XMovement = moveInput * movementSpeed;   

        return XMovement;
    }

    private void HandleHeadCollisions()
    {
        Ray ray = new Ray(transform.position, Vector3.up * raycastSize);
        Debug.DrawRay(ray.origin, ray.direction * raycastSize);
        RaycastHit[] hits = Physics.RaycastAll(ray, raycastSize);
        if (hits.Length == 0){return;}
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                fallVelocity = gravity * Time.fixedDeltaTime;
                break;
            }
        }
    }

    private void HandleInputs()
    {
        //Movimiento
        moveInput = Input.GetAxis("Horizontal");
        //Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            if (characterController.isGrounded){
                
                jumpRequest = true;
                isDoubleJumpSpent = false;

            } else if(!isDoubleJumpSpent){
                
                jumpRequest = true;                
                isDoubleJumpSpent = true; 
                isOnPlatform = false;              

            }
        } 
    }

    
    IEnumerator damageFlash(){

        int times = flashNumber;
        Debug.Log("Flashing");
        while (times > 0){

            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(4 * Time.deltaTime);    
            gameObject.GetComponent<Renderer>().enabled = true;            
            yield return null;

            times--;

        }

    }

    public override float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public override void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public bool SetJumpRequest(){

        return jumpRequest;

    }

    public void SetJumpRequest(bool newValue){
    
        jumpRequest = newValue;

    }
}
