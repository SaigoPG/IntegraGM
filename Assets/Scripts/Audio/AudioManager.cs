using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent (typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] soundList;

    private int position = 0;
    private int sampleRate;
    private AudioSource audioSource;
    private Sound currentSound;

    private void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        foreach (var sound in soundList)
        {
            if (sound.soundName == soundName)
            {
                currentSound = sound;
                audioSource.clip = AudioClip.Create(soundName, (int)(sound.duration * sampleRate), 1, sampleRate, true, OnAudioRead, OnAudioSetPosition);
                audioSource.Play();
            }
        }
    }

    public void Stop()
    {
        audioSource.Stop();
        currentSound = null;
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = currentSound.GenerateSound((float)position / sampleRate);
            position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)    {
        position = newPosition;
    }
}