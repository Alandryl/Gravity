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

            SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
            soundPlayer.audioToPlay = soundPlayer.audioClipWallTransition;
            soundPlayer.PlayAudio();
        }

        if (other.tag == "Pickup")
        {
            other.gameObject.GetComponent<Item>().ResetPosition();

            SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
            soundPlayer.audioToPlay = soundPlayer.audioClipWallTransition;
            soundPlayer.PlayAudio();
        }
    }
}
