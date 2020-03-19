using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    Activation activator;
    AudioSource audioSource;

    public bool isMoving;

    public GameObject LiftObject;
    public GameObject Point1;
    public GameObject Point2;

    public float moveSpeed = 4;

    public GameObject targetPoint;

    public AudioClip audioclipMoving;
    public AudioClip audioclipStop;

    void Start()
    {
        targetPoint = Point1;
        activator = GetComponent<Activation>();
        audioSource = LiftObject.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (activator.activated)
        {
            MoveLift();
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            LiftObject.transform.position = Vector3.MoveTowards(LiftObject.transform.position, targetPoint.transform.position, step);
        }

        if (LiftObject.transform.position == targetPoint.transform.position && isMoving)
        {
            StopLift();
        }
    }

    public void MoveLift()
    {
        audioSource.clip = null;
        audioSource.clip = audioclipMoving;
        audioSource.Play();

        activator.activated = false;

        isMoving = true;
        
        if (targetPoint == Point1)
        {
            targetPoint = Point2;
        }
        else
        {
            targetPoint = Point1;
        }
    }

    public void StopLift()
    {
        isMoving = false;
        audioSource.clip = null;
        audioSource.PlayOneShot(audioclipStop);
    }
}
