using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenButton : MonoBehaviour
{
    Activation activator;
    AudioSource audioSource;
    public Stats stats;

    public GameObject objectToActivate;


    public int requiredGravityCores;
    public Text requiredGravityCoresText;

    [Header("Screens")]
    public GameObject ActivateScreen;
    public GameObject DeniedScreen;

    [Header("Audio")]
    public AudioClip audioActivated;
    public AudioClip audioDenied;

    void Start()
    {
        activator = GetComponent<Activation>();
        audioSource = GetComponent<AudioSource>();
        stats = FindObjectOfType<Stats>();

        if (activator == null)
        {
            activator = GetComponentInChildren<Activation>();
        }

        string gravityCoresRequiredString = requiredGravityCores.ToString();
        requiredGravityCoresText.text = gravityCoresRequiredString;
    }

    void Update()
    {
        if (activator.activated)
        {
            if (stats.gravityCoresCollected >= requiredGravityCores)
            {
                buttonPressed();
            }
            else
            {
                StartCoroutine(Denied());
            }
        }
    }

    public void buttonPressed()
    {
        activator.activated = false;
        objectToActivate.GetComponent<Activation>().activated = true;
    }


    public IEnumerator Denied()
    {
        activator.activated = false;

        ActivateScreen.SetActive(false);
        DeniedScreen.SetActive(true);
        audioSource.PlayOneShot(audioDenied);

        yield return new WaitForSeconds(2f);

        ActivateScreen.SetActive(true);
        DeniedScreen.SetActive(false);
    }
}
