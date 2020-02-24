using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    AudioSource audioSource;

    [Header("Audio Clips")]

    public AudioClip audioToPlay;

    public AudioClip audioClipWallTransition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audioSource.PlayOneShot(audioToPlay);
    }
    

}
