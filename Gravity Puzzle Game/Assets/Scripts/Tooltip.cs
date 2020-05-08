using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    Activation activation;

    bool activated;
    public bool oneTimePlay = true;

    public float displayTime = 8;


    [TextArea]
    public string tooltilText;

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
        tooltip.SetActive(true);

        tooltip.GetComponent<Animator>().SetBool("Visible", true);
        tooltipTextObject.GetComponent<Text>().text = tooltilText.ToString();

        yield return new WaitForSeconds(displayTime);

        tooltip.GetComponent<Animator>().SetBool("Visible", false);

        activation.activated = false;

        if (!oneTimePlay)
        {
            activated = false;
        }
    }
}