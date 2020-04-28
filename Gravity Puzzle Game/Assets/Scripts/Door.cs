using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    Activation activator;

    public bool automaticOpen;

    public AudioClip audioclipOpen;
    bool open;


    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        activator = GetComponent<Activation>();
    }


    void Update()
    {
        if (activator.activated)
        {
            Open();
        }
    }


    public void Open()
    {
        activator.activated = false;
        open = !open;

        if(open)
        {
            anim.SetBool("isOpen", true);
        }
        else
        {
            anim.SetBool("isOpen", false);
        }

        audioSource.PlayOneShot(audioclipOpen);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && automaticOpen)
        {
            open = true;
            anim.SetBool("isOpen", true);
            audioSource.PlayOneShot(audioclipOpen);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && automaticOpen)
        {
            open = false;
            anim.SetBool("isOpen", false);
            audioSource.PlayOneShot(audioclipOpen);
        }
    }

}
