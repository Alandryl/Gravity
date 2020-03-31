using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool carryingObject;
    public bool canPickup;
    public bool canInteract;

    public GameObject playerCamera;
    public GameObject placementSpot;
    public GameObject selectableObject;
    public GameObject selectedObject;
    public GameObject pickupSlot;

    public float pickUpRange = 3;



    public bool canPlace;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //Raycast hit pickup

        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * pickUpRange, Color.yellow);

        

        if (Physics.Raycast(ray, out hit, pickUpRange) && selectedObject == null)
        {
            if (hit.collider.tag == "Pickup" || hit.collider.tag == "Interactable" || hit.collider.transform.parent.gameObject.tag == "Pickup")
            {

                selectableObject = hit.collider.gameObject;

                if (hit.collider.transform.parent.gameObject.tag == "Pickup")
                {
                    selectableObject = hit.collider.gameObject.GetComponentInParent<Item>().gameObject;
                }

                if (selectableObject.tag == "Pickup")
                {
                    canPickup = true;
                }
                if (selectableObject.tag == "Interactable")
                {
                    canInteract = true;
                }
            }
        }
        else
        {
            selectableObject = null;
            canPickup = false;
            canInteract = false;
        }

        /*
        if (Physics.Raycast(ray, out hit, pickUpRange) && selectedObject == null)
        {
            if (hit.collider.tag == "Pickup" || hit.collider.tag == "Interactable" || hit.collider.transform.parent.gameObject.tag == "Pickup")
            {
                selectableObject = hit.collider.gameObject;

                if (hit.collider.transform.parent.gameObject.tag == "Pickup")
                {
                    selectableObject = hit.collider.transform.parent.gameObject;
                }

                if (selectableObject.tag == "Pickup")
                {
                    canPickup = true;
                }
                if (selectableObject.tag == "Interactable")
                {
                    canInteract = true;
                }
            }
        }
        else
        {
            selectableObject = null;
            canPickup = false;
            canInteract = false;
        }
        */













        //Pickup


        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null && selectableObject != null && canPickup)
            {
                carryingObject = true;
                selectedObject = selectableObject;
                //selectedObject.GetComponent<Collider>().enabled = false;

                selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                selectedObject.GetComponent<Item>().pickedUp = true;
            }
            else if (selectedObject != null && carryingObject)
            {
                if (canPlace)
                {
                    carryingObject = false;
                    PlacePickup();
                }
            }
        }

        if (carryingObject)
        {
            //selectedObject.transform.position = pickupSlot.transform.position;
            //selectedObject.transform.rotation = pickupSlot.transform.rotation;

            selectedObject.transform.rotation = Quaternion.Lerp(selectedObject.transform.rotation, pickupSlot.transform.rotation, Time.deltaTime * 20);
            selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, pickupSlot.transform.position, Time.deltaTime * 20);
        }




        //Interact

        
        if (Input.GetMouseButtonDown(0))
        {
            if (canInteract)
            {
                selectableObject.GetComponent<Activation>().activated = true;
            }
        }
        


    }



    void PlacePickup()

    {
        GameObject objectBeingPlaced = selectedObject;
        //objectBeingPlaced.transform.position = placementSpot.transform.position;

        objectBeingPlaced.GetComponent<Collider>().enabled = true;

        Vector3 vec = objectBeingPlaced.transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        objectBeingPlaced.transform.eulerAngles = vec;

        //objectBeingPlaced.transform.position = Vector3.Lerp(objectBeingPlaced.transform.position, placementSpot.transform.position, Time.deltaTime);

        //objectBeingPlaced.transform.position = Vector3.Lerp(objectBeingPlaced.transform.position, placementSpot.transform.position, Time.deltaTime);

        selectedObject.GetComponent<Item>().pickedUp = false;

        selectedObject = null;
    }
}





