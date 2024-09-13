using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SineWave", menuName = "Audio/Waves/Sine Wave")]
public class SineWave : WaveData
{
    public override float GetWaveValue(float time)
    {
        float value = 0f;
        float phase = 2 * Mathf.PI * frequency * time;
        value = Mathf.Sin(phase);
        return value * amplitude;
    }
}
