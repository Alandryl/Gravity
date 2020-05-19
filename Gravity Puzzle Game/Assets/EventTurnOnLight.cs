using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTurnOnLight : MonoBehaviour
{
    public List<Animator> lights;
    bool activated;

    void Start()
    {
        foreach (Animator lightAnimator in lights)
        {
            lightAnimator.SetBool("On", false);
        }
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            Activate();
        }
    }

    public void Activate()
    {
        activated = true;

        foreach (Animator lightAnimator in lights)
        {
            lightAnimator.SetBool("On", true);
        }
    }
}
