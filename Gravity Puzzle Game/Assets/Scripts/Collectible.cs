using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
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
            SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
            soundPlayer.audioToPlay = soundPlayer.audioClipWallTransition;
            soundPlayer.PlayAudio();

            Destroy(gameObject);
        }
    }
}
