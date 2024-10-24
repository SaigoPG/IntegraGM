using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool pauseOnStart;
    [SerializeField] private bool capturedMouse;
    [SerializeField] private MenuManager menuManager;



    public bool pause { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangePauseMode(pauseOnStart);
        ChangeMouseMode(capturedMouse);
    }

    public void ChangeMouseMode(bool mode)
    {
        if (mode)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ChangePauseMode(bool mode)
    {
        if (mode)
        {
            Time.timeScale = 0.0f;
            pause = true;
            if (menuManager != null) menuManager.Transition("InPause");
        }
        else
        {
            Time.timeScale = 1.0f;
            pause = false;
            if (menuManager != null) menuManager.Transition("InGame");
        }
    }
}
