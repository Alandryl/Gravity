using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Activator activator;
    AudioSource audioSource;

    Quaternion targetAngle;
    public float rotationSpeed = 3;

    public AudioClip soundActivation;

    void Start()
    {
        activator = GetComponent<Activator>();
        audioSource = GetComponent<AudioSource>();
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

    }

    public void Rotate()
    {
        activator.activated = false;
        targetAngle *= Quaternion.Euler(0, 90, 0);
        audioSource.Stop();
        audioSource.PlayOneShot(soundActivation);
    }
}
