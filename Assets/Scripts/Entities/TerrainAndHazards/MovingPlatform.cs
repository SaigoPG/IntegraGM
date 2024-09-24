using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class MovingPlatform : Entity
{   

    [SerializeField] private bool isTrolling;
    [SerializeField] private float movementDistance;
    [SerializeField] private float thresholdDistance;   
    [SerializeField] private Player plRef;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    private Vector3 currDistanceVector;
    private Vector3 currPosVector;
    private Vector3 prevPosVector;

    Vector3 startPos, endPos, velocity;
    private float currDistanceMagnitude;
    private bool director;
    private float step;

    public bool getTrolling(){

        return isTrolling;

    }

    public Vector3 getVelocity(){

        return velocity;

    }

    protected override void Movement()
    {
        step = movementSpeed * Time.deltaTime;        

        if(isTrolling){ //Is a trolling platform

            if(currDistanceMagnitude < Mathf.Pow(thresholdDistance, 2)){    

                float currPos = currPosVector.x;
                float newPos = currPos + currDistanceVector.x;
                Vector3 newPosition = new Vector3(newPos, currPosVector.y, currPosVector.z);
                gameObject.transform.position = Vector3.MoveTowards(currPosVector, newPosition, step);

            }

        } else {  //Is a regular platform            

            if(director){

                Vector3 direction = (endPos - currPosVector).normalized;
                gameObject.transform.Translate(direction * step);
                if(Vector3.Distance(currPosVector, endPos) < 1){

                    director = false;

                }

            } else {

                Vector3 direction = (startPos - currPosVector).normalized;
                gameObject.transform.Translate(direction * step);
                if(Vector3.Distance(currPosVector, startPos) < 1){

                    director = true;

                }

            }             

        }
    }    

    private void CalcDistance(){

        currDistanceVector = gameObject.transform.position - plRef.gameObject.transform.position;
        currDistanceMagnitude = currDistanceVector.sqrMagnitude;

    }

    private void CalcVelocity(){

        velocity = (currPosVector - prevPosVector)/Time.fixedDeltaTime;
        prevPosVector = currPosVector;

    }

    void Start()
    {
        
        if(!isTrolling){

            startPos = start.transform.position;
            endPos = end.transform.position;            
        
        }

        currPosVector = gameObject.transform.position;

    }

    
    void Update()
    {
        
        

    }

    void FixedUpdate(){

        CalcVelocity();

        if(isTrolling){ 
            
            CalcDistance();

        }
        
        currPosVector = gameObject.transform.position;  
              

        Movement(); 

    }

    public override float GetMovementSpeed()
    {
        throw new System.NotImplementedException();
    }

    public override void SetMovementSpeed(float newSpeed)
    {
        throw new System.NotImplementedException();
    }

}
