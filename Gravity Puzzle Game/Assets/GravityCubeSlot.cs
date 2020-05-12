using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCubeSlot : MonoBehaviour
{
    Activation activation;

    public Item gravityCube;

    void Start()
    {
        activation = GetComponent<Activation>();
    }

    void Update()
    {
        if (activation.activated)
        {
            gravityCube.ResetPosition();
            activation.activated = false;
        }
    }


}
