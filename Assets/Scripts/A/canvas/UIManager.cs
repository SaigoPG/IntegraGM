using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject CinematicaCanvas;
    public GameObject mapCanvas;
    public GameObject OpcionesCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject AnuncioCanvas;
    public GameObject ControlesCanvas;
    public GameObject recompensaCanvas;


    private GameObject currentActiveCanvas;

    
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowCanvas(mainMenuCanvas);
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

    public void ShowMainMenu()
    {
        ShowCanvas(mainMenuCanvas);
    }

    public void ShowCinematica()
    {
        ShowCanvas(CinematicaCanvas);
    }

    public void Showmap()
    {
        ShowCanvas(mapCanvas);
    }

    public void ShowSettings()
    {
        ShowCanvas(OpcionesCanvas);
    }

    public void ShowPauseMenu()
    {
        ShowCanvas(pauseMenuCanvas);
    }

    public void ShowAnuncio()
    {
        ShowCanvas(AnuncioCanvas);
    }

    public void ShowControles()
    {
        ShowCanvas(ControlesCanvas);
    }

    public void ShowRecompensa()
    {
        ShowCanvas(recompensaCanvas);
    }
}
