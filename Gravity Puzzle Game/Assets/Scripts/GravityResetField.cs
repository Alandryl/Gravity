using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityResetField : MonoBehaviour
{
    public GravityDirection gravityDirection;

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
            GameObject playerObject = other.gameObject;
            PlayerMovementScriptNew playerMovementScriptNew = playerObject.GetComponent<PlayerMovementScriptNew>();

            if (playerMovementScriptNew.gravityDirection != gravityDirection)
            {
                playerMovementScriptNew.gravityDirection = gravityDirection;
            }
        }
    }
}
