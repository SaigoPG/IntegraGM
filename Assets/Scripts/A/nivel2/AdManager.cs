using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameObject adPopup; 
    public Image adImage; 
    public Button adButton; 
    public Button closeButton; 
    public List<Sprite> adPopupImages; 
    public List<string> adPopupURLs; 
    public float minTimeBetweenAds = 5f; 
    public float maxTimeBetweenAds = 15f; 
    private bool isAdActive = false;
    private int currentImageIndex = 0; 
    public float initialDelay = 15f; 
    public float timeBeforeCloseButtonAppears = 3f; 

    void Start()
    {
        adPopup.SetActive(false);
        closeButton.gameObject.SetActive(false); 

        StartCoroutine(ShowAdsWithInitialDelay());

        adButton.onClick.AddListener(OpenAdURL);

        closeButton.onClick.AddListener(CloseAd);
    }

    IEnumerator ShowAdsWithInitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        StartCoroutine(ShowAdsWithInterval());
    }

    IEnumerator ShowAdsWithInterval()
    {
        float timeToWait = Random.Range(minTimeBetweenAds, maxTimeBetweenAds);
        yield return new WaitForSeconds(timeToWait);

        ShowAd();

        yield return new WaitForSecondsRealtime(timeBeforeCloseButtonAppears);
        closeButton.gameObject.SetActive(true);
    }

    void ShowAd()
    {
        if (!adPopup.activeSelf)
        {
            adPopup.SetActive(true);
        }

        Time.timeScale = 0;

        if (adPopupImages.Count > 0)
        {
            adImage.sprite = adPopupImages[currentImageIndex];
            currentImageIndex = (currentImageIndex + 1) % adPopupImages.Count; 
        }

        isAdActive = true;
        closeButton.gameObject.SetActive(false); 
    }

    void CloseAd()
    {
        adPopup.SetActive(false); 
        isAdActive = false;

        Time.timeScale = 1;

        StartCoroutine(RestartAdCycle());
    }

    IEnumerator RestartAdCycle()
    {
        float timeToWait = Random.Range(minTimeBetweenAds, maxTimeBetweenAds);
        yield return new WaitForSeconds(timeToWait);

        StartCoroutine(ShowAdsWithInterval());
    }

    void OpenAdURL()
    {
        if (adPopupURLs.Count > currentImageIndex)
        {
            string url = adPopupURLs[currentImageIndex - 1];
            Application.OpenURL(url); // Abre la URL en el navegador
        }
    }
}