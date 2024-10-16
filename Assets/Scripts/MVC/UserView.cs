// UserView.cs
using UnityEngine;
using UnityEngine.UI;

public class UserView : MonoBehaviour
{
    // Referencias a los elementos de UI
    public InputField nombreInput;
    public InputField edadInput;
    public Button guardarButton;
    public Button cargarButton;
    public Text mostrarDatosText;

    private UserController controller;

    private void Start()
    {
        controller = new UserController();

        // Asignar eventos a los botones
        guardarButton.onClick.AddListener(OnGuardarClicked);
        cargarButton.onClick.AddListener(OnCargarClicked);
    }

    // Evento al hacer clic en guardar
    private void OnGuardarClicked()
    {
        string nombre = nombreInput.text;
        int edad;

        if (int.TryParse(edadInput.text, out edad))
        {
            UserData data = new UserData
            {
                nombre = nombre,
                edad = edad
            };

            controller.SaveUserData(data);
            mostrarDatosText.text = "Datos guardados correctamente.";
        }
        else
        {
            mostrarDatosText.text = "Por favor, ingresa una edad válida.";
        }
    }

    // Evento al hacer clic en cargar
    private void OnCargarClicked()
    {
        UserData data = controller.LoadUserData();
        if (data != null)
        {
            mostrarDatosText.text = $"Nombre: {data.nombre}\nEdad: {data.edad}";
        }
        else
        {
            mostrarDatosText.text = "No se encontraron datos para cargar.";
        }
    }
}
