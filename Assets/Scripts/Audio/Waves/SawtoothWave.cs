using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SawtoothWave", menuName = "Audio/Waves/Sawtooth Wave")]
public class SawtoothWave : WaveData
{
    public override float GetWaveValue(float time)
    {
        float value = 0f;
        float phase = 2 * Mathf.PI * frequency * time;
        value = 2f * (time * frequency - Mathf.Floor(time * frequency + 0.5f));
        return value * amplitude;
    }
}
