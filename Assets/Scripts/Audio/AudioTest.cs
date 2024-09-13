using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.Play("Test");
    }

}
