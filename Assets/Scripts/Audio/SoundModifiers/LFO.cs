using UnityEngine;

[CreateAssetMenu(fileName = "LFOModifier", menuName = "Audio/Modifiers/LFO")]
public class LFO: SoundModifier
{
    [SerializeField] private WaveData waveData;
    public override float ApplyModification(float time)
    {
        // Aplica la modulación con la amplitud y el valor central
        return midValue + waveData.GetWaveValue(time);
    }
}