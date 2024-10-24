using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : SlideMenu
{
    public void ChangeLevel(int level)
    {
        AudioManager.Instance.EmitEffect("BtnSound");
        SceneManager.LoadScene(level);
    }
}
