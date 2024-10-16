using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class controlmusica : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ControlMusica (float slidermusica)
    {
        audioMixer.SetFloat("Volumenmusica", Mathf.Log10(slidermusica)*20);
    }
}
