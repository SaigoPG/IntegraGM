using UnityEngine;

public class UIManagerdi : MonoBehaviour
{
    // Array de GameObjects que representa todos los Canvases a gestionar
    public GameObject[] canvases;

    // El Canvas actualmente activo
    private GameObject currentActiveCanvas;

    // Instancia Singleton del UIManager
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            //Instance = this;  // Asigna esta instancia como la instancia única
            DontDestroyOnLoad(gameObject);  // Evita que este GameObject sea destruido al cambiar de escena
        }
        else
        {
            Destroy(gameObject);  // Si ya existe una instancia, destruye este GameObject
        }
    }

    private void Start()
    {
        // Si hay al menos un Canvas en el array, muestra el primero al iniciar
        if (canvases.Length > 0)
        {
            ShowCanvas(canvases[0]);  // Muestra el primer Canvas (por ejemplo, el menú principal)
        }
    }

    // Método para mostrar un Canvas específico
    public void ShowCanvas(GameObject canvasToShow)
    {
        // Si hay un Canvas activo, desactívalo
        if (currentActiveCanvas != null)
        {
            currentActiveCanvas.SetActive(false);
        }

        // Activa el Canvas seleccionado
        canvasToShow.SetActive(true);
        currentActiveCanvas = canvasToShow;  // Actualiza el Canvas actualmente activo
    }

    // Método para mostrar un Canvas por su índice en el array
    public void ShowCanvasByIndex(int index)
    {
        // Verifica que el índice esté dentro de los límites del array
        if (index >= 0 && index < canvases.Length)
        {
            ShowCanvas(canvases[index]);
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango: " + index);
        }
    }

    // Métodos para mostrar Canvases específicos desde otros scripts o botones
    public void ShowMainMenu()
    {
        ShowCanvas(canvases[0]);  // Suponiendo que el MainMenuCanvas es el primero
    }

    public void ShowCinematica()
    {
        ShowCanvas(canvases[1]);  // Suponiendo que el CinematicaCanvas es el segundo
    }

    public void ShowMap()
    {
        ShowCanvas(canvases[2]);  // Suponiendo que el MapCanvas es el tercero
    }

    public void ShowSettings()
    {
        ShowCanvas(canvases[3]);  // Suponiendo que el SettingsCanvas es el cuarto
    }

    public void ShowPauseMenu()
    {
        ShowCanvas(canvases[4]);  // Suponiendo que el PauseMenuCanvas es el quinto
    }

    public void ShowAnuncio()
    {
        ShowCanvas(canvases[5]);  // Suponiendo que el AnuncioCanvas es el sexto
    }

    public void ShowControles()
    {
        ShowCanvas(canvases[6]);  // Suponiendo que el ControlesCanvas es el séptimo
    }

    public void ShowRecompensa()
    {
        ShowCanvas(canvases[7]);  // Suponiendo que el RecompensaCanvas es el octavo
    }
}
