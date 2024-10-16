using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeAudioEmitter : MonoBehaviour
{
    private AudioOrganizer audioOrganizer;
    private Dictionary<string, StudioEventEmitter> emitters = new Dictionary<string, StudioEventEmitter>();
    private void Awake()
    {
        audioOrganizer = new AudioOrganizer();
    }

    private void Start()
    {
        audioOrganizer.OrganizeAudios(this, emitters);
    }

    public void emitSound(string soundName)
    {
        if (!emitters.ContainsKey(soundName))
        {
            Debug.LogWarning("El sonido " + soundName + " no se encuentra en la lista");
            return;
        }
        emitters[soundName].Play();
    }
}
