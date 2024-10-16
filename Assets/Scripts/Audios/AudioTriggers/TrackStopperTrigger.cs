using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackStopperTrigger : AudioTrigger
{
    protected override void Interact()
    {
        AudioManager.Instance.StopCurrentTrack();
    }
}
