using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseMenu : MonoBehaviour
{
    [SerializeField] protected bool activeOnAwake;
    [SerializeField] protected float transitionDuration;

    protected CanvasGroup canvasGroup;
    public bool activated {  get; private set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        activated = activeOnAwake;
        changeCanvasGroupCharateristics(activeOnAwake);
        _Awake();
    }

    protected abstract void _Awake();

    public void FadeIn()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        activated = true;
        OnFadeIn();
    }

    public void FadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        activated = false;
        OnFadeOut();
    }

    private void changeCanvasGroupCharateristics(bool mode)
    {
        canvasGroup.interactable = mode;
        canvasGroup.blocksRaycasts = mode;
    }

    protected abstract void OnFadeIn();
    protected abstract void OnFadeOut();
}
