using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public GravityDirection gravityDirection;

    public GameObject destination;
    public GameObject player;
    public GameObject placementSpot;
    public bool pickedUp;
    public bool grounded;

    void Start()
    {
        destination = GameObject.Find("PickupPivot");
        player = GameObject.Find("Player");
        placementSpot = GameObject.Find("PlacementSpot");
    }

    void Update()
    {
        if (pickedUp)
        {
            transform.position = destination.transform.position;
            transform.rotation = destination.transform.rotation;
            gravityDirection = player.GetComponent<PlayerMovementScriptNew>().currentGravityDirection;
        }





        if (Input.GetMouseButtonDown(0))
        {
            if (!pickedUp)
            {
                if (gravityDirection == player.GetComponent<PlayerMovementScriptNew>().currentGravityDirection)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    this.transform.parent = destination.transform;
                    pickedUp = true;
                }
            }
            else if (pickedUp)
            {
                GetComponent<BoxCollider>().enabled = true;
                this.transform.parent = null;
                pickedUp = false;
                transform.position = placementSpot.transform.position;
            }
        }
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

}
