using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  

public class CoinManager : MonoBehaviour
{
    public int coins = 10;  
    public float interval = 4f;  
    public TextMeshProUGUI coinText; 
    public GameObject gameOverPanel;  

    private float timeSinceLastCoinDecrease;

    void Start()
    {
        timeSinceLastCoinDecrease = 0f;
        UpdateCoinText();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        timeSinceLastCoinDecrease += Time.deltaTime;

        if (timeSinceLastCoinDecrease >= interval)
        {
            timeSinceLastCoinDecrease = 0f;
            DecreaseCoins();
        }
    }

    void DecreaseCoins()
    {
        if (coins > 0)
        {
            coins-=10;
            UpdateCoinText();

            if (coins == 0)
            {
                GameOver();
            }
        }
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "" + coins;
        }
        else
        {
            Debug.LogError("coinText no está asignado en el inspector.");
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame() 
    {
        Time.timeScale = 1;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }
}
