using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquareWave", menuName = "Audio/Waves/Square Wave")]
public class SquareWave : WaveData
{
    public override float GetWaveValue(float time)
    {
        float value = 0f;
        float phase = 2 * Mathf.PI * frequency * time;
        value = Mathf.Sign(Mathf.Sin(phase));
        return value * amplitude;
    }
}
