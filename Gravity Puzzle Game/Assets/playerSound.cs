using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSound : MonoBehaviour
{
    PlayerMovementScriptNew movementScript;

    //Audio
    AudioSource audioSource;
    public AudioClip audioStep;

    float footstepTimer = 0.5f;
    float footstepCountdown = 0f;

    void Start()
    {
        movementScript = GetComponent<PlayerMovementScriptNew>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Apply footstep sound
        if (footstepCountdown > 0)
        {
            footstepCountdown -= Time.deltaTime;
        }

        if (movementScript.grounded &&
            footstepCountdown <= 0 &&
            (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            audioSource.pitch = (Random.Range(0.8f, 1.2f));
            audioSource.volume = (Random.Range(0.8f, 1f));
            audioSource.PlayOneShot(audioStep);
            footstepCountdown = footstepTimer;
        }
    }
}
