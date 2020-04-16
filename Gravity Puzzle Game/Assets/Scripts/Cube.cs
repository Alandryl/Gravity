using System.Collections;
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

        if (!IsGrounded())
        {
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

        if (IsGrounded() && itemScript.pickedUp == false && !active)
        {
            Activate();
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
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            gravityDirectionVector = new Vector3(-gravity * rb.mass, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            gravityDirectionVector = new Vector3(0, gravity * rb.mass, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            gravityDirectionVector = new Vector3(0, -gravity * rb.mass, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            gravityDirectionVector = new Vector3(0, 0, gravity * rb.mass);
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            gravityDirectionVector = new Vector3(0, 0, -gravity );
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && itemScript.pickedUp == false)
        {
            //transform.parent = collision.transform;
            Activate();
        }
    }

    void Activate()
    {
        active = true;
        cube.SetActive(true);
        //rb.isKinematic = true;
        audioSource.PlayOneShot(audioActivate);
    }


    public bool IsGrounded()
    {
        float DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        Vector3 raycastStart = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z); ;

        if (gravityDirection == GravityDirection.XPlus)
        {
            raycastStart = new Vector3(transform.localPosition.x - 0.1f, transform.localPosition.y, transform.localPosition.z);
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            raycastStart = new Vector3(transform.localPosition.x + 0.1f, transform.localPosition.y, transform.localPosition.z);
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            raycastStart = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.1f, transform.localPosition.z);
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            raycastStart = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z);
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            raycastStart = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.1f);
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            raycastStart = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.1f);
        }

        Debug.DrawRay(raycastStart, -transform.up * 1, Color.green);
        return Physics.Raycast(raycastStart, -transform.up, DistanceToTheGround - 0.25f);

    }

}
