using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    AudioSource audioSource;
    public AudioSource ambience;

    public bool playerInside;

    [Header("Audio Clips")]

    public AudioClip audioToPlay;
    public AudioClip ambienceToPlay;

    public AudioClip audioClipWallTransition;
    public AudioClip audioDeath;
    public AudioClip audioMessageIntro;
    public AudioClip audioMessageOutro;
    public AudioClip audioAmbianceInside;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInside)
        {
            ambienceToPlay = audioAmbianceInside;
        }
        
        if (!playerInside)
        {
            ambienceToPlay = null;
        }

        if (ambience.clip != ambienceToPlay)
        {
            PlayAmbience();
        }

    }

    public void PlayAudio()
    {
        audioSource.PlayOneShot(audioToPlay);
    }

    public void PlayAmbience()
    {
        ambience.clip = ambienceToPlay;
        ambience.Play();
    }
}
