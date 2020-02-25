using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Activation activator;
    Animator anim;
    AudioSource audioSource;

    public GameObject objectToActivate;

    public float resetTime = 0.3f;
    public float timeTillReset = 0f;

    public AudioClip soundActivation;

    void Start()
    {
        activator = GetComponent<Activation>();
        audioSource = GetComponent<AudioSource>();

        if (activator == null)
        {
            activator = GetComponentInChildren<Activation>();
        }

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (activator.activated && timeTillReset > 0)
        {
            activator.activated = false;
        }

        if (activator.activated && timeTillReset <= 0)
        {
            buttonPressed();
        }

        if (timeTillReset > 0)
        {
            timeTillReset -= Time.deltaTime;
        }
    }

    public void buttonPressed()
    {
        timeTillReset = resetTime;
        anim.ResetTrigger("Activated");
        activator.activated = false;
        objectToActivate.GetComponent<Activation>().activated = true;
        anim.SetTrigger("Activated");
        audioSource.PlayOneShot(soundActivation);
    }
}
