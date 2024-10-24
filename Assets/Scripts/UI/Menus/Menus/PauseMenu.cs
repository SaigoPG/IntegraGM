using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : FadeMenu
{
    [SerializeField] bool FP;
    public void ResumeBtn()
    {
        GameManager.Instance.ChangePauseMode(false);
        if (FP) GameManager.Instance.ChangeMouseMode(true);
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene(0);
    }
}
