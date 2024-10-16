using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private CodeAudioEmitter AmbientEmitters;
    [SerializeField] private CodeAudioEmitter MusicEmitters;
    [SerializeField] private CodeAudioEmitter EffectsEmitters;

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

    public void EmitEffect(string soundName)
    {
        EffectsEmitters.emitSound(soundName);
    }

    public void EmitAmbient(string soundName)
    {
        AmbientEmitters.emitSound(soundName);
    }

    public void EmitMusic(string soundName)
    {
        MusicEmitters.emitSound(soundName);
    }
}
