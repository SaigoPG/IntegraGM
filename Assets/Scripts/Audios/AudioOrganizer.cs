using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOrganizer
{
    public void OrganizeAudios(CodeAudioEmitter codeEmitter, Dictionary<string, StudioEventEmitter> dictionary)
    {
        StudioEventEmitter[] emitters = codeEmitter.GetComponentsInChildren<StudioEventEmitter>();
        foreach (StudioEventEmitter emitter in emitters) dictionary[emitter.name] = emitter;
    }
}
