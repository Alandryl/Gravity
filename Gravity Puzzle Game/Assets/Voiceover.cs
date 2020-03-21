using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voiceover : MonoBehaviour
{
    public SoundPlayer soundPlayer;
    public AudioSource audioSource;

    public AudioClip dialogueToPlay;
    public string dialogueText;

    bool activated;
    public bool IntroOutro = true;


    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
        audioSource = soundPlayer.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            StartCoroutine(ActivateVoiceover());

        }
    }

    public IEnumerator ActivateVoiceover()
    {
        activated = true;

        GameObject subtitles = GameObject.Find("Subtitles");
        subtitles.GetComponent<Text>().text = dialogueText.ToString();

        if (IntroOutro)
        {
            audioSource.PlayOneShot(soundPlayer.audioMessageIntro);
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        subtitles.GetComponent<Animator>().SetBool("Visible", true);
        audioSource.PlayOneShot(dialogueToPlay);

        yield return new WaitWhile(() => audioSource.isPlaying);

        if (IntroOutro)
        {
            audioSource.PlayOneShot(soundPlayer.audioMessageOutro);
        }

        subtitles.GetComponent<Animator>().SetBool("Visible", false);
    }
}
