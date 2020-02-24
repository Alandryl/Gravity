using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFloorScript : MonoBehaviour
{
    public GravityDirection gravityDirection;
    public Transform direction;

    public Vector3 finalVector;

    void Start()
    {

    }

    void Update()
    {
        Vector3 positionVector = transform.position;
        Vector3 directionVector = direction.position;

        finalVector = positionVector - directionVector;


        if (finalVector.x > 0.9f)
        {
            gravityDirection = GravityDirection.XPlus;
        }
        if (finalVector.x < -0.9f)
        {
            gravityDirection = GravityDirection.XMinus;
        }
        if (finalVector.y > 0.9f)
        {
            gravityDirection = GravityDirection.YPlus;
        }
        if (finalVector.y < -0.9f)
        {
            gravityDirection = GravityDirection.YMinus;
        }
        if (finalVector.z > 0.9f)
        {
            gravityDirection = GravityDirection.ZPlus;
        }
        if (finalVector.z < -0.9f)
        {
            gravityDirection = GravityDirection.ZMinus;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject playerObject = collision.gameObject;
            PlayerMovementScriptNew playerMovementScriptNew = playerObject.GetComponent<PlayerMovementScriptNew>();

            if (playerMovementScriptNew.currentGravityDirection != gravityDirection && playerMovementScriptNew.grounded)
            {
                if (playerMovementScriptNew.gravityChangeCooldownLeft <= 0)
                {
                    playerMovementScriptNew.gravityDirection = gravityDirection;


                    SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
                    soundPlayer.audioToPlay = soundPlayer.audioClipWallTransition;
                    soundPlayer.PlayAudio();
                }
            }
        }
    }
}
