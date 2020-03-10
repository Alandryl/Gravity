using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    public bool pickedUp = false;

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
        GetComponent<Rigidbody>().isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
