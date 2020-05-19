using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    public bool pickedUp = false;

    public bool reset;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {

    }

    public void ResetPosition()
    {
        PickUp pickup = FindObjectOfType<PickUp>();
        pickup.selectedObject = null;
        pickedUp = false;
        //GetComponent<Rigidbody>().isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;

        reset = true;
    }
}
