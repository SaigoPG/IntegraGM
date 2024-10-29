using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class anuncio : MonoBehaviour
{
    public VideoPlayer videoPlayer;       
    public Canvas videoCanvas;           
    public Canvas resultCanvas;          
    public CoinManager coinManager;      
    public AudioSource ambientMusic;      
    public GameObject skipButton;         
    public float timeBeforeSkipButton = 5f;  
    public int monedasGanadas = 20;       

    private bool videoPlaying = false;    

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += OnVideoEnd;  
        resultCanvas.gameObject.SetActive(false);   
        skipButton.SetActive(false);                
    }

    public void PlayVideo()
    {
        videoCanvas.gameObject.SetActive(true);   
        videoPlayer.Play();                      
        Time.timeScale = 0;                       
        videoPlaying = true;

        if (ambientMusic != null)
        {
            ambientMusic.mute = true;
        }

        skipButton.SetActive(false);
        StartCoroutine(ShowSkipButtonAfterDelay());
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoCanvas.gameObject.SetActive(false);  
        resultCanvas.gameObject.SetActive(true);   
        videoPlaying = false;

        if (coinManager != null)
        {
            coinManager.AddCoins(monedasGanadas);
        }
    }

    public void ContinueGame()
    {
        resultCanvas.gameObject.SetActive(false);  
        Time.timeScale = 1;                       

        if (ambientMusic != null)
        {
            ambientMusic.mute = false;
        }
    }

    private IEnumerator ShowSkipButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(timeBeforeSkipButton);  
        skipButton.SetActive(true);  
    }

    public void SkipVideo()
    {
        videoPlayer.Stop();                 
        OnVideoEnd(videoPlayer);            
    }
}
