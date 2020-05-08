using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFloorSelection : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject elevatorObject;

    [Header("Buttons")]
    public int selectedFloor = 2;

    public Activation floor1;
    public bool floor1Unlocked = false;
    public Activation floor2;
    public bool floor2Unlocked = false;
    public Activation floor3;
    public bool floor3Unlocked = false;


    [Header("Screens")]
    public GameObject floorSelectionScreen;
    public GameObject DeniedScreen;

    [Header("Audio")]
    public AudioClip audioActivated;
    public AudioClip audioDenied;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (floor1.activated)
        {
            if (floor1Unlocked)
            {
                buttonPressed();
                selectedFloor = 1;
            }
            else
            {
                StartCoroutine(Denied());
            }
            floor1.activated = false;
        }

        if (floor2.activated)
        {
            if (floor2Unlocked)
            {
                buttonPressed();
                selectedFloor = 2;
            }
            else
            {
                StartCoroutine(Denied());
            }
            floor2.activated = false;
        }

        if (floor3.activated)
        {
            if (floor3Unlocked)
            {
                buttonPressed();
                selectedFloor = 3;
            }
            else
            {
                StartCoroutine(Denied());
            }
            floor3.activated = false;
        }
    }

    public void buttonPressed()
    {
        audioSource.PlayOneShot(audioActivated);
    }


    public IEnumerator Denied()
    {
        floorSelectionScreen.SetActive(false);
        DeniedScreen.SetActive(true);
        audioSource.PlayOneShot(audioDenied);

        yield return new WaitForSeconds(2f);

        floorSelectionScreen.SetActive(true);
        DeniedScreen.SetActive(false);
    }
}
