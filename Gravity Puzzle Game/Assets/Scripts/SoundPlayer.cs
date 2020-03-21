﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    AudioSource audioSource;

    [Header("Audio Clips")]

    public AudioClip audioToPlay;
    public AudioClip audioClipWallTransition;
    public AudioClip audioDeath;
    public AudioClip audioMessageIntro;
    public AudioClip audioMessageOutro;


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
