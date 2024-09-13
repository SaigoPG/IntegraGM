using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound")]
public class Sound : ScriptableObject
{
    public string soundName;
    public float duration;
    [SerializeField] private WaveData[] waveDataList;
    [SerializeField] private SoundModifier[] volumeModifiers;

    public float GenerateSound(float currentTime)
    {
        float soundValue = 0;
        foreach (var waveData in waveDataList)
        {
            soundValue += waveData.GetWaveValue(currentTime);
        }
        return soundValue;
    }
}
