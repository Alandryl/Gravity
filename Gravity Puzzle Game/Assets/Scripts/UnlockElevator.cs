using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockElevator : MonoBehaviour
{
    public bool unlockFloor1;
    public bool unlockFloor2;
    public bool unlockFloor3;

    public ElevatorFloorSelection elevatorFloorSelection;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (unlockFloor1)
            {
                elevatorFloorSelection.floor1Unlocked = true;
            }
            if (unlockFloor2)
            {
                elevatorFloorSelection.floor2Unlocked = true;
            }
            if (unlockFloor3)
            {
                elevatorFloorSelection.floor3Unlocked = true;
            }
        }
    }
}
