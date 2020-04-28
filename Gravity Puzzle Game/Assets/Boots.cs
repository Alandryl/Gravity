using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour
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
            GameObject.Find("Stats").GetComponent<Stats>().hasGravityBoots = true;

            SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
            soundPlayer.audioToPlay = soundPlayer.audioMessageOutro;
            soundPlayer.PlayAudio();

            soundPlayer.audioToPlay = soundPlayer.audioClipWallTransition;

            soundPlayer.PlayAudio();

            Destroy(gameObject);
        }
    }
}
