using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSoundFootsteps;
    [FMODUnity.EventRef]
    public string inputSoundJump;

    PlayerMovementScriptNew movementScript;
    public float walkingSpeed = 0.5f;


    void Start()
    {
        movementScript = GetComponent<PlayerMovementScriptNew>();

        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    void Update()
    {
        if (movementScript.grounded && movementScript.canJump && Input.GetButtonDown("Jump"))
        {
            CallJump();
        }
    }


    void CallFootsteps()
    {
        if (movementScript.grounded &&
            (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputSoundFootsteps);
        }
    }

    void CallJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot(inputSoundJump);
    }
}
