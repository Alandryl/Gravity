using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangerScript : MonoBehaviour
{
    [SerializeField]
    private GravityDirection gravityDirection1;
    [SerializeField]
    private GravityDirection gravityDirection2;


    public bool oneWay;

    public GameObject direction1;
    public GameObject direction2;

    public float rechargeTime = 2f;
    public float rechargeTimeLeft;

    AudioSource audioSource;
    public AudioClip soundActivation; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rechargeTimeLeft >= 0)
        {
            rechargeTimeLeft -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (rechargeTimeLeft <= 0)
            {
                GameObject playerObject = collision.gameObject;
                PlayerMovementScriptNew playerMovementScriptNew = playerObject.GetComponent<PlayerMovementScriptNew>();

                if (playerMovementScriptNew.currentGravityDirection == gravityDirection1)
                {
                    playerMovementScriptNew.newRotation = direction1.transform.rotation;
                    playerMovementScriptNew.gravityDirection = gravityDirection2;
                }
                else if (playerMovementScriptNew.currentGravityDirection == gravityDirection2 && !oneWay)
                {
                    playerMovementScriptNew.newRotation = direction2.transform.rotation;
                    playerMovementScriptNew.gravityDirection = gravityDirection1;
                }

                rechargeTimeLeft = rechargeTime;
                audioSource.PlayOneShot(soundActivation);

            }
        }
    }
}
