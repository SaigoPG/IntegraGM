using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPause : FadeMenu
{
    MenuManager menuManager;
    protected override void _Awake()
    {
        base._Awake();
        menuManager = GetComponent<MenuManager>();
    }

    protected override void OnFadeOut()
    {
        base.OnFadeOut();
        menuManager.Transition("Pause");
    }
}