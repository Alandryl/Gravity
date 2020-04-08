﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    public bool grounded;

    public GameObject cube;
    Item itemScript;

    public bool active = false;

    public GravityDirection gravityDirection = GravityDirection.YMinus;
    public Vector3 gravityDirectionVector;
    public float gravity = 10.0f;

    public AudioClip audioActivate;


    float timeToActivate;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        itemScript = GetComponent<Item>();

        ChangeGravity();
    }

    void Update()
    {

        if (itemScript.pickedUp == true)
        {
            gravityDirection = FindObjectOfType<PlayerMovementScriptNew>().gravityDirection;
            ChangeGravity();

            transform.parent = null;
            rb.freezeRotation = false;
            cube.SetActive(false);
            active = false;
        }

        if (itemScript.pickedUp == false && active == false)
        {
            rb.freezeRotation = true;
            rb.isKinematic = false;

        }

    }

    void FixedUpdate()
    {
        if (!active && itemScript.pickedUp == false)
        {
            rb.AddForce(gravityDirectionVector, ForceMode.Acceleration);
        }


        if (rb.velocity.y == 0 && !active && itemScript.pickedUp == false)
        {
            timeToActivate -= Time.deltaTime;

            if (timeToActivate <= 0f)
            {
                Activate();
            }
        }
        else
        {
            timeToActivate = 3f;
        }
       
    }

    void ChangeGravity()
    {
        if (gravityDirection == GravityDirection.XPlus)
        {
            gravityDirectionVector = new Vector3(gravity * rb.mass, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            gravityDirectionVector = new Vector3(-gravity * rb.mass, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            gravityDirectionVector = new Vector3(0, gravity * rb.mass, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            gravityDirectionVector = new Vector3(0, -gravity * rb.mass, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            gravityDirectionVector = new Vector3(0, 0, gravity * rb.mass);
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            gravityDirectionVector = new Vector3(0, 0, -gravity );
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && itemScript.pickedUp == false)
        {
            transform.parent = collision.transform;
            Activate();
        }
    }

    void Activate()
    {
        active = true;
        cube.SetActive(true);
        rb.isKinematic = true;
        audioSource.PlayOneShot(audioActivate);
    }


}
