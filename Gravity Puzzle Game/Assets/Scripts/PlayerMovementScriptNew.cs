using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScriptNew : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public bool grounded = false;

    Rigidbody rb;

    public GravityDirection gravityDirection;
    public GravityDirection currentGravityDirection;

    public Quaternion currentRotation;
    public Quaternion newRotation;
    bool rotating = false;
    public float rotationTime = 2;
    public float rotationTimeLeft = 0;

    public GameObject rotateTowards;

    public Vector3 gravityDirectionVector;

    float gravityChangeCooldown = 1;
    [HideInInspector] public float gravityChangeCooldownLeft = 0f;


    Vector3 velocity;






    //TEST
    private float rotationTimer;
    private Quaternion startRotation;









    void Start()
    {
        gravityDirectionVector = new Vector3(0, -gravity * rb.mass, 0);
        newRotation = Quaternion.Euler(0, 0, 0);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void Update()
    {
        // Jump

        if (grounded)
        {
            if (canJump && Input.GetButtonDown("Jump"))
            {
                if (gravityDirection == GravityDirection.XPlus)
                {
                    rb.velocity = new Vector3(-CalculateJumpVerticalSpeed(), velocity.y, velocity.z);
                }
                if (gravityDirection == GravityDirection.XMinus)
                {
                    rb.velocity = new Vector3(CalculateJumpVerticalSpeed(), velocity.y, velocity.z);
                }
                if (gravityDirection == GravityDirection.YPlus)
                {
                    rb.velocity = new Vector3(velocity.x, -CalculateJumpVerticalSpeed(), velocity.z);
                }
                if (gravityDirection == GravityDirection.YMinus)
                {
                    rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
                if (gravityDirection == GravityDirection.ZPlus)
                {
                    rb.velocity = new Vector3(velocity.x, velocity.y, -CalculateJumpVerticalSpeed());
                }
                if (gravityDirection == GravityDirection.ZMinus)
                {
                    rb.velocity = new Vector3(velocity.x, velocity.y, CalculateJumpVerticalSpeed());
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);








        if (gravityDirection == GravityDirection.XPlus)
        {
            velocityChange.x = 0;
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            rotateTowards.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            velocityChange.x = 0;
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            rotateTowards.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rotateTowards.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rotateTowards.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = 0;
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            rotateTowards.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = 0;
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            rotateTowards.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }




        rb.AddForce(velocityChange, ForceMode.VelocityChange);

       

        //Grounded

        if (IsGrounded())
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        //Gravity

        rb.AddForce(gravityDirectionVector, ForceMode.Acceleration);



        //Change Rotation

        /*
        
        currentRotation = transform.rotation;


        if (rotationTimeLeft > 0)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, newRotation, rotationTime * 0.1f);
            rotationTimeLeft -= Time.deltaTime;
        }
        if (rotationTimeLeft > 0)
        {
            currentRotation = newRotation;
        }

        */


        /*
        if (rotationTimeLeft > 0)
        {

            Vector3 rotateDirection = (rotateTowards.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.FromToRotation(Vector3.up * -1, rotateDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);



            //transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * rotationSpeed);



            rotationTimeLeft -= Time.deltaTime;
        }
        */
        
        if (rotationTimer <= rotationTime)
        {
            rotationTimer += Time.fixedDeltaTime;
            rotationTimeLeft -= Time.deltaTime;

            Vector3 rotateDirection = (rotateTowards.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.FromToRotation(Vector3.up * -1, rotateDirection);
            //Quaternion toRotation = Quaternion.LookRotation(Vector3.up * -1, rotateDirection);

            transform.rotation = Quaternion.Lerp(startRotation, toRotation, rotationTimer / rotationTime);

        }
        






        //Change Gravity

        if (gravityDirection != currentGravityDirection)
        {
            currentGravityDirection = gravityDirection;
            ChangeGravity();
        }

        if (gravityChangeCooldownLeft > 0)
        {
            gravityChangeCooldownLeft -= Time.deltaTime;
        }
    }

    /*
    void OnCollisionStay()
    {
        grounded = true;
    }
    */

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




    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }




    void ChangeGravity()
    {

        if (gravityDirection == GravityDirection.XPlus)
        {
            gravityDirectionVector = new Vector3(gravity, 0, 0);
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            gravityDirectionVector = new Vector3(-gravity, 0, 0);
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            gravityDirectionVector = new Vector3(0, gravity, 0);
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            gravityDirectionVector = new Vector3(0, -gravity, 0);
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            gravityDirectionVector = new Vector3(0, 0, gravity);
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            gravityDirectionVector = new Vector3(0, 0, -gravity);
        }

        gravityChangeCooldownLeft = gravityChangeCooldown;
        rotationTimeLeft = rotationTime;






        //TEST



        startRotation = transform.rotation;
        rotationTimer = 0;


    }

}
