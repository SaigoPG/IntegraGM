using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour 
{
    public float timer = 60; 
    public TextMeshProUGUI textotimer;
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            int minutos = Mathf.FloorToInt(timer / 60);
            int segundos = Mathf.FloorToInt(timer % 60);
            textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        textotimer.text = "00:00"; 
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
