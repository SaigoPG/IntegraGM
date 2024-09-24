using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChangerTrigger : AudioTrigger
{
    [SerializeField] int newTrack = 0;
    protected override void Interact()
    {
        AudioManager.Instance.ChangeTrack(newTrack);
    }
}
