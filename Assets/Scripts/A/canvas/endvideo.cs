using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class endvideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
    public Canvas videoCanvas;       
    public Canvas nextCanvas;       

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        
        videoPlayer.loopPointReached += OnVideoEnd;

        nextCanvas.gameObject.SetActive(false);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoCanvas.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
    }
}

