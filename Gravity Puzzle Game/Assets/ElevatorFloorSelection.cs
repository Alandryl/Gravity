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
    public Activation floor2;
    public Activation floor3;

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
            floor1.activated = false;
            buttonPressed();
            selectedFloor = 1;
        }

        if (floor2.activated)
        {
            floor2.activated = false;
            buttonPressed();
            selectedFloor = 2;
        }

        if (floor3.activated)
        {
            floor3.activated = false;
            buttonPressed();
            selectedFloor = 3;
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
