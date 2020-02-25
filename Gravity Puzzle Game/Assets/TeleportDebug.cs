using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDebug : MonoBehaviour
{
    public GameObject player;

    public GameObject Teleport1;
    public GameObject Teleport2;
    public GameObject Teleport3;
    public GameObject Teleport4;
    public GameObject Teleport5;
    public GameObject Teleport6;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("1") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport1.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
        if (Input.GetButton("2") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport2.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
        if (Input.GetButton("3") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport3.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
        if (Input.GetButton("4") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport4.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
        if (Input.GetButton("5") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport5.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
        if (Input.GetButton("6") && Input.GetButton("Jump"))
        {
            player.transform.position = Teleport6.transform.position;
            player.GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;
        }
    }
}
