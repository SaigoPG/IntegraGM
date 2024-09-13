using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TriangleWave", menuName = "Audio/Waves/Triangle Wave")]
public class TriangleWave : WaveData
{
    public override float GetWaveValue(float time)
    {
        float value = 0f;
        float phase = 2 * Mathf.PI * frequency * time;
        value = Mathf.PingPong(2 * phase, 2f) - 1f;
        return value * amplitude;
    }
}
