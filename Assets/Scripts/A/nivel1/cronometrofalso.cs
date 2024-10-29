using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class cronometrofalso : MonoBehaviour
{
    public float Timer = 60;
    public TextMeshProUGUI textotimer;

    void Update()
    {

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            int minutos = Mathf.FloorToInt(Timer / 60);
            int segundos = Mathf.FloorToInt(Timer % 60);
            textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
        else
        {
            Timer = 0;
            int minutos = Mathf.FloorToInt(Timer / 60);
            int segundos = Mathf.FloorToInt(Timer % 60);
            textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }
}