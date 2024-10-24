using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMenu : BaseMenu
{
    protected override void _Awake()
    {
        
    }

    private void Start()
    {
        if (activeOnAwake) canvasGroup.alpha = 1f;
        else canvasGroup.alpha = 0f;
    }
    protected override void OnFadeOut()
    {
        print("Ejecutando animacion out");
        LeanTween.value(gameObject, canvasGroup.alpha, 0f, transitionDuration)
                 .setIgnoreTimeScale(true)
                 .setOnUpdate((float value) => {
                     canvasGroup.alpha = value;
                 });
    }
    protected override void OnFadeIn()
    {
        print("Ejecutando animacion in");
        LeanTween.value(gameObject, canvasGroup.alpha, 1f, transitionDuration)
                 .setIgnoreTimeScale(true)
                 .setOnUpdate((float value) => {
                     canvasGroup.alpha = value;
                 });
    }
}
