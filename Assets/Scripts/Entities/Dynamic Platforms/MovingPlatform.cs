using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : Entity
{

    [SerializeField] private GameObject anchorA, anchorB;
    private Vector3 currPlatformPos, prevPlatformPos, velocity;

    private Vector3 anchorAPos, anchorBPos;

    private bool arrivedB;

    protected override void Movement()
    {
        
        if(!arrivedB){

            gameObject.transform.position = Vector3.MoveTowards(currPlatformPos, anchorBPos, movementSpeed * Time.fixedDeltaTime);   

        } else{

            gameObject.transform.position = Vector3.MoveTowards(currPlatformPos, anchorAPos, movementSpeed * Time.fixedDeltaTime);

        }

    }

    public Vector3 GetVelocity(){

        return velocity;

    }

    void CalcVelocity(){

        velocity = (currPlatformPos - prevPlatformPos)/Time.fixedDeltaTime;
        prevPlatformPos = currPlatformPos;

    }

    void OnTriggerEnter(Collider col){

        if(!col.gameObject.CompareTag("Player")){

                if(col.gameObject == anchorB){

                arrivedB = true;

            } else {

                arrivedB = false;
                
            }

        }

    }

    void Start(){

        anchorAPos = anchorA.transform.position;
        gameObject.transform.position = anchorAPos;
        anchorBPos = anchorB.transform.position;
        currPlatformPos = gameObject.transform.position;
        prevPlatformPos = currPlatformPos;

    }

    void FixedUpdate(){
        
        CalcVelocity();
        
        currPlatformPos = gameObject.transform.position;
        Movement();
        

    }
}
