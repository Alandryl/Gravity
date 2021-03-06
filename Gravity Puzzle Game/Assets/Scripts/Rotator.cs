﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Activation activator;
    AudioSource audioSource;

    Quaternion currentAngle;
    Quaternion targetAngle;
    public float rotationSpeed = 3;

    public AudioClip soundActivation;

    void Start()
    {
        activator = GetComponent<Activation>();
        audioSource = GetComponent<AudioSource>();
        currentAngle = transform.rotation;
        targetAngle = transform.rotation;
    }

    void FixedUpdate()
    {
        if (activator.activated)
        {
            Rotate();
        }

        //Rotate
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * rotationSpeed);

        if (currentAngle == targetAngle)
        {
            currentAngle = targetAngle;
        }
    }

    public void Rotate()
    {
        activator.activated = false;
        targetAngle *= Quaternion.Euler(0, 90, 0);
        audioSource.Stop();
        audioSource.PlayOneShot(soundActivation);
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        targetAngle = currentAngle;
    }
    */
}