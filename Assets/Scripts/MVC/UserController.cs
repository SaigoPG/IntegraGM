// UserController.cs
using System.IO;
using UnityEngine;

public class UserController
{
    private string filePath;

    public UserController()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userdata.json");
    }

    // Guardar datos en JSON
    public void SaveUserData(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Datos guardados en: " + filePath);
    }

    // Cargar datos desde JSON
    public UserData LoadUserData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("Datos cargados desde: " + filePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Archivo JSON no encontrado en: " + filePath);
            return null;
        }
    }
}
