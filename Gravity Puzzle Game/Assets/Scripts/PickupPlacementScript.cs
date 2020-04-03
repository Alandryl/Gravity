using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlacementScript : MonoBehaviour
{
    PickUp pickupScript;

    public GameObject player;
    public GameObject GroundPlacerPivot;
    public float raycastHeight = 2;
    public GravityDirection gravityDirection;

    public Vector3 gravityDirectionVector;

    void Start()
    {
        pickupScript = GetComponentInParent<PickUp>();        
    }

    void Update()
    {
        gravityDirection = player.GetComponent<PlayerMovementScriptNew>().currentGravityDirection;


        RaycastHit hit;


        Ray groundRay = new Ray(transform.position, gravityDirectionVector);


        //Gravity Direction

        if (gravityDirection == GravityDirection.XPlus)
        {
            gravityDirectionVector = new Vector3(1, 0, 0);
        }
        if (gravityDirection == GravityDirection.XMinus)
        {
            gravityDirectionVector = new Vector3(-1, 0, 0);
        }
        if (gravityDirection == GravityDirection.YPlus)
        {
            gravityDirectionVector = new Vector3(0, 1, 0);
        }
        if (gravityDirection == GravityDirection.YMinus)
        {
            gravityDirectionVector = new Vector3(0, -1, 0);
        }
        if (gravityDirection == GravityDirection.ZPlus)
        {
            gravityDirectionVector = new Vector3(0, 0, 1);
        }
        if (gravityDirection == GravityDirection.ZMinus)
        {
            gravityDirectionVector = new Vector3(0, 0, -1);
        }







        if (Physics.Raycast(groundRay, out hit, raycastHeight))
        {
            if(hit.collider.tag == "Ground")
            {
                GroundPlacerPivot.transform.position = hit.point;
                pickupScript.canPlace = true;
            }
            else
            {
                pickupScript.canPlace = true;
            }
        }
    }
}
