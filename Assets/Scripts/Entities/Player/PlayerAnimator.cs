using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake(){

        animator = GetComponent<Animator>();

    }

    public void UpdateAnimations(float animationInput, float fallVelocity, bool isGrounded){

        animator.SetBool("IsJumping", !isGrounded && fallVelocity > 0);
        animator.SetFloat("movement", animationInput);
        animator.SetBool("OnFloor", isGrounded);
        animator.SetBool("IsFalling", !isGrounded && fallVelocity < 0);

        if(animationInput < 0 && transform.localScale.x >0){

            transform.localScale = new Vector3(-1,1,1);

        } else if(animationInput > 0 && transform.localScale.x < 0) {

            transform.localScale = new Vector3(1,1,1);

        }

    }
}
