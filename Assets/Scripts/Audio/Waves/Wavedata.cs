using UnityEngine;

public abstract class WaveData : ScriptableObject
{
    public float frequency = 440f;  // Frecuencia en Hz
    [SerializeField] protected float amplitude = 1f;  // Amplitud de la onda

    // Funci�n para obtener el valor de la onda en el tiempo actual
    public virtual float GetWaveValue(float time) { return 0; }
}