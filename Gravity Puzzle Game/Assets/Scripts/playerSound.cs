using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSound : MonoBehaviour
{
    AudioSource audio;

    [FMODUnity.EventRef]
    public string inputSoundFootsteps;
    [FMODUnity.EventRef]
    public string inputSoundJump;

    PlayerMovementScriptNew movementScript;
    public float walkingSpeed = 0.5f;

    bool isWalking;
    public float StepStartDelay = 0.3f;
    float timeTillFirstStep;


    public AudioClip audioResetGravity;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        movementScript = GetComponent<PlayerMovementScriptNew>();

        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    void Update()
    {
        if (movementScript.grounded && movementScript.canJump && Input.GetButtonDown("Jump"))
        {
            CallJump();
        }

        if (isWalking)
        {
            timeTillFirstStep += Time.deltaTime;
        }
        else
        {
            timeTillFirstStep = 0;
        }


    }


    void CallFootsteps()
    {
        if (movementScript.grounded &&
            (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            isWalking = true;

            if (timeTillFirstStep >= StepStartDelay)
            {
                FMODUnity.RuntimeManager.PlayOneShot(inputSoundFootsteps);
            }
        }
        else
        {
            isWalking = false;
        }
    }

    public void CallJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot(inputSoundJump);
    }

    public void CallLanding()
    {
        FMODUnity.RuntimeManager.PlayOneShot(inputSoundFootsteps);
    }

    public void ResetGravity()
    {
        audio.PlayOneShot(audioResetGravity);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Indoor")
        {
            GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>().playerInside = true;
        }
        else
        {
            GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>().playerInside = false;
        }
    }
}
