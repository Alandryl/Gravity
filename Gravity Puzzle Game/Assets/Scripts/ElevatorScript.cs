using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    Activation activator;
    AudioSource audioSource;
    Animator anim;

    public bool isMoving;
    public float moveSpeed = 12;


    public ElevatorFloorSelection elevatorSelectionScreen;

    [Header("Floors")]

    public int currentFloor = 2;
    public GameObject floor1;
    public GameObject floor1Door;
    public GameObject floor2;
    public GameObject floor2Door;
    public GameObject floor3;
    public GameObject floor3Door;

    public GameObject targetPoint;

    [Header("Audio")]

    public AudioClip audioclipMoving;
    public AudioClip audioclipStop;

    void Start()
    {
        activator = GetComponent<Activation>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        targetPoint = floor2;
        OpenDoor();
    }

    void Update()
    {
        if (currentFloor != elevatorSelectionScreen.selectedFloor)
        {
            StartCoroutine(MoveElevator());
        }
        
        if (currentFloor == 1)
        {
            targetPoint = floor1;
        }
        if (currentFloor == 2)
        {
            targetPoint = floor2;
        }
        if (currentFloor == 3)
        {
            targetPoint = floor3;
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.transform.position, step);
        }


        if (transform.position.normalized == targetPoint.transform.position.normalized && isMoving)
        {
            StartCoroutine(StopElevator());
        }

    }

    public void OpenDoor()
    {
        anim.SetBool("isOpen", true);

        if (currentFloor == 1)
        {
            floor1Door.GetComponent<Animator>().SetBool("isOpen", true);
        }
        if (currentFloor == 2)
        {
            floor2Door.GetComponent<Animator>().SetBool("isOpen", true);
        }
        if (currentFloor == 3)
        {
            floor3Door.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }
    public void CloseDoor()
    {
        anim.SetBool("isOpen", false);

        if (currentFloor == 1)
        {
            floor1Door.GetComponent<Animator>().SetBool("isOpen", false);
        }
        if (currentFloor == 2)
        {
            floor2Door.GetComponent<Animator>().SetBool("isOpen", false);
        }
        if (currentFloor == 3)
        {
            floor3Door.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }


    public IEnumerator MoveElevator()
    {
        CloseDoor();
        currentFloor = elevatorSelectionScreen.selectedFloor;

        yield return new WaitForSeconds(2);

        audioSource.clip = null;
        audioSource.clip = audioclipMoving;
        audioSource.Play();
        isMoving = true;
    }

    public IEnumerator StopElevator()
    {
        isMoving = false;
        audioSource.clip = null;
        audioSource.PlayOneShot(audioclipStop);

        yield return new WaitForSeconds(2);

        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
