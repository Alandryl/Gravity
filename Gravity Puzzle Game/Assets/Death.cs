using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool dead;
    bool respawning;
    public GameObject respawnPoint;
    public GameObject BlackScreen;

    void Start()
    {
        BlackScreen = GameObject.Find("BlackScreen");
    }

    void Update()
    {
        if (dead && !respawning)
        {
            StartCoroutine(Die()) ;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            dead = true;
        }
    }



    IEnumerator Die()
    {
        GetComponent<PlayerMovementScriptNew>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;

        SoundPlayer soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
        soundPlayer.audioToPlay = soundPlayer.audioDeath;
        soundPlayer.PlayAudio();

        respawning = true;
        yield return new WaitForSeconds(0.5f);
        BlackScreen.GetComponent<Animator>().SetBool("Black", true);
        yield return new WaitForSeconds(1f);

        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        BlackScreen.GetComponent<Animator>().SetBool("Black", false);
        yield return new WaitForSeconds(1f);
        dead = false;
        respawning = false;

        GetComponent<PlayerMovementScriptNew>().gravityDirection = GravityDirection.YMinus;

        GetComponent<PlayerMovementScriptNew>().enabled = true;
        GetComponentInChildren<MouseLook>().enabled = true;
    }
}
