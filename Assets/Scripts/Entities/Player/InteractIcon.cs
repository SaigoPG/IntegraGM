using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CodeAudioEmitter))]
public class InteractIcon : MonoBehaviour, IInteractable
{
    [SerializeField] private float animationTime = 0.2f;

    private bool visible = false;
    private CodeAudioEmitter soundEmitter;
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 1);
        soundEmitter = GetComponent<CodeAudioEmitter>();
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
            soundEmitter.emitSound("InteractIconSound");
        }
        else
        {
            LeanTween.scaleX(gameObject, 0, animationTime).setEaseOutExpo();
            LeanTween.scaleY(gameObject, 0, animationTime).setEaseOutExpo();
        }
    }
}