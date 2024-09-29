using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrollPlatform : Entity
{   
    [SerializeField] private GameObject triggerHolder;

    private Vector3 currPos, objPos;
    private bool isPlayerChased;

    protected override void Movement()
    {
        
        if(isPlayerChased){

            currPos = gameObject.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(currPos, objPos, -(movementSpeed * Time.deltaTime));  

        }

    }


    void OnTriggerStay(Collider col){

        if(col.gameObject.GetComponent<Player>() != null){

            objPos = col.gameObject.transform.position;

            isPlayerChased = true;

        }

    }

    void OnTriggerExit(Collider col){

        isPlayerChased = false;

    }

    void Update(){

        Movement();

    }

}
