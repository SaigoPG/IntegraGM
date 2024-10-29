using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controles;
    public GameObject opciones;
    public int numesc;

    private GameObject currentActiveCanvas;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowCanvas(GameObject canvasToShow)
    {
        if (currentActiveCanvas != null)
        {
            currentActiveCanvas.SetActive(false);
        }

        canvasToShow.SetActive(true);
        currentActiveCanvas = canvasToShow;
    }

    public void ShowControles()
    {
        Time.timeScale = 0;
        ShowCanvas(controles);
    }

    public void Showopciones()
    {
        Time.timeScale = 0;
        ShowCanvas(opciones);
    }

    public void salir()
    {
        SceneManager.LoadScene(numesc);
    }

    public void ReturnToPauseMenu()
    {
        ShowCanvas(pausePanel); 
    }

}