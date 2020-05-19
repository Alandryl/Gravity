using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    bool pickedUp;
    bool inSlot = true;

    public bool grounded;

    public GameObject cube;
    Item itemScript;

    public bool active = false;

    public GravityDirection gravityDirection = GravityDirection.YMinus;
    public Vector3 gravityDirectionVector;
    public float gravity = 10.0f;

    public AudioClip audioActivate;


    float timeToActivate;

    public float ActivationPause = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        itemScript = GetComponent<Item>();

        ChangeGravity();
        Reset();
    }

    void Update()
    {

        if (itemScript.pickedUp == true && !pickedUp)
        {
            Pickup();
        }

        if (pickedUp)
        {
            gravityDirection = FindObjectOfType<PlayerMovementScriptNew>().gravityDirection;
            ChangeGravity();
        }

        if (!IsGrounded())
        {
            transform.parent = null;
            cube.SetActive(false);
            active = false;
        }

        
        if (itemScript.pickedUp == false && pickedUp)
        {
            Drop();
        }

        if (IsGrounded() && itemScript.pickedUp == false && !active && timeToActivate <= 0f)
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

        if (timeToActivate > 0f)
        {
            timeToActivate -= Time.deltaTime;
        }


        if (itemScript.reset)
        {
            Reset();
        }






        /*
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
            timeToActivate = ActivationPause;
        }
        */

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
            gravityDirectionVector = new Vector3(0, 0, -gravity * rb.mass);
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

    void Pickup()
    {
        timeToActivate = ActivationPause;

        inSlot = false;
        pickedUp = true;

        transform.parent = null;
        rb.freezeRotation = false;
        cube.SetActive(false);
        active = false;
    }

    void Drop()
    {
        pickedUp = false;
        rb.freezeRotation = true;
    }

    void Activate()
    {

        active = true;

        if (!inSlot)
        {
            cube.SetActive(true);
        }

        rb.freezeRotation = true;
        audioSource.PlayOneShot(audioActivate);


        Vector3 vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;

    }

    void Reset()
    {
        itemScript.reset = false;


        gravityDirection = GravityDirection.YMinus;
        ChangeGravity();

        inSlot = true;
        timeToActivate = 0f;
        cube.SetActive(false);
        active = true;
        rb.freezeRotation = true;
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
