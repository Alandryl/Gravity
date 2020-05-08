using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    Activation activation;

    bool activated;
    public bool oneTimePlay = true;

    public string tooltilText;

    public float displayTime = 8;

    void Start()
    {
        activation = GetComponent<Activation>();
    }

    void Update()
    {
        if (activation.activated && !activated)
        {
            StartCoroutine(ActivateTooltip());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            StartCoroutine(ActivateTooltip());
        }
    }


    public IEnumerator ActivateTooltip()
    {
        activation.activated = false;
        activated = true;
        GameObject tooltip = GameObject.Find("Tooltip");
        GameObject tooltipTextObject = GameObject.Find("TooltipText");

        tooltip.GetComponent<Animator>().SetBool("Visible", true);
        tooltipTextObject.GetComponent<Text>().text = tooltilText.ToString();

        yield return new WaitForSeconds(2);

        tooltip.GetComponent<Animator>().SetBool("Visible", false);

        activation.activated = false;

        if (!oneTimePlay)
        {
            activated = false;
        }       
    }
}