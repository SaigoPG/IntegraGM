using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource[] tracks;
    [SerializeField] private float fadeDuration = 1.0f;
    [Header("On Awake")]
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private int trackOnAwake = 0;

    private int currentindexTrack = -1;

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
        if (playOnAwake && trackOnAwake >= 0 && trackOnAwake < tracks.Length)
        {
            currentindexTrack = trackOnAwake;
            ChangeTrackMode(tracks[trackOnAwake], true);
        }
        else if (playOnAwake)
        {
            Debug.LogWarning("Track On Awake fuera de rango: " + trackOnAwake);
        }
    }

    private void ChangeTrackMode(AudioSource track, bool activate)
    {
        if (activate)
        {
            StartCoroutine(FadeIn(track));
        }
        else
        {
            StartCoroutine(FadeOut(track));
        }
    }

    public void ChangeTrack(int index)
    {
        if (index < 0 || index >= tracks.Length)
        {
            Debug.LogWarning("Index fuera de rango: " + index);
            return;
        }
        if (tracks[index].isPlaying)
        {
            Debug.LogWarning("El index " + index + "es de un track que ya se esta reproduciendo");
            return;
        }
        currentindexTrack = index;
        StopCurrentTrack();
        ChangeTrackMode(tracks[index], true);
    }

    public void StopCurrentTrack()
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].isPlaying)
            {
                ChangeTrackMode(tracks[i], false);
                return;
            }
        }
    }

    public void ChangeVelocity(int index, float newVelocity) //Solo modifica pitch por ahora
    {
        if (index < 0 || index >= tracks.Length)
        {
            Debug.LogWarning("Index fuera de rango: " + index);
            return;
        }
        tracks[index].pitch = newVelocity;
    }
    
    private IEnumerator FadeOut(AudioSource pista)
    {
        if (pista == null) yield break;
        float startVolume = pista.volume;
        while (pista.volume > 0)
        {
            pista.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        pista.Stop();
        pista.volume = startVolume;
    }
    
    private IEnumerator FadeIn(AudioSource pista)
    {
        pista.Play();
        float startVolume = 0f;
        pista.volume = startVolume;

        while (pista.volume < 1)
        {
            pista.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
        pista.volume = 1f;
    }
}