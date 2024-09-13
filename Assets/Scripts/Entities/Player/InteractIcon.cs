using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIcon : MonoBehaviour, IInteractable
{
    [SerializeField] private float animationTime = 0.2f;

    private bool visible = false;
    void Start()
    {
        transform.localScale = new Vector3(0,0,1);
    }

    public void Interact()
    {
        visible = !visible;
        HandleAnimation();

    }

    private void HandleAnimation()
    {
        if (visible)
        {
            LeanTween.scaleX(gameObject, 1, animationTime).setEaseOutExpo();
            LeanTween.scaleY(gameObject, 1, animationTime).setEaseOutExpo();
        }
        else
        {
            LeanTween.scaleX(gameObject, 0, animationTime).setEaseOutExpo();
            LeanTween.scaleY(gameObject, 0, animationTime).setEaseOutExpo();
        }
    }
}
