using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundModifier : ScriptableObject
{
    [HideInInspector] public float midValue { protected get; set; }//Ver como hacer que solo se pueda modificar y no ver
    public virtual float ApplyModification(float time) { return 0; }
}