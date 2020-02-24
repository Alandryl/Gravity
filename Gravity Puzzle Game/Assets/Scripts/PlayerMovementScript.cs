using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;



    public enum GravityDirection { XPlus, XMinus, YPlus, YMinus, ZPlus, ZMinus };
    public GravityDirection gravityDirection;
    public GravityDirection currentGravityDirection;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentGravityDirection = gravityDirection;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);



        //Gravity

    
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }



        //velocity += new Vector3(0, gravity, z) * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;



        controller.Move(velocity * Time.deltaTime);


        /*
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (gravityDirection == GravityDirection.XPlus)
            {
                velocity.x = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            if (gravityDirection == GravityDirection.XMinus)
            {
                velocity.x = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            if (gravityDirection == GravityDirection.YPlus)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            if (gravityDirection == GravityDirection.YMinus)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            if (gravityDirection == GravityDirection.ZPlus)
            {
                velocity.z = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            if (gravityDirection == GravityDirection.ZMinus)
            {
                velocity.z = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        if (gravityDirection == GravityDirection.XPlus)
        {
            velocity.x -= gravity * Time.deltaTime;
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            velocity.x += gravity * Time.deltaTime;
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            velocity.z -= gravity * Time.deltaTime;
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            velocity.z += gravity * Time.deltaTime;
        }     
        */

        if (gravityDirection != currentGravityDirection)
        {
            currentGravityDirection = gravityDirection;
            ChangeGravity();
        }
    }

    void ChangeGravity()
    {

        if (gravityDirection == GravityDirection.XPlus)
        {

        }
        if (gravityDirection == GravityDirection.XMinus)
        {

        }
        if (gravityDirection == GravityDirection.YPlus)
        {

        }
        if (gravityDirection == GravityDirection.YMinus)
        {

        }
        if (gravityDirection == GravityDirection.ZPlus)
        {

        }
        if (gravityDirection == GravityDirection.ZMinus)
        {

        }


        print("IT WORKS");
    }
}
