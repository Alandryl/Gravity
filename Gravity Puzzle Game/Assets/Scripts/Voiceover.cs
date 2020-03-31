using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voiceover : MonoBehaviour
{
    public SoundPlayer soundPlayer;
    public AudioSource audioSource;
    Activation activation;

    bool activated;
    public bool oneTimePlay = true;
    public bool IntroOutro = true;


    //List of all AudioClips
    public List<AudioClip> DialogueClipList = new List<AudioClip>();
    //List of all valid directories
    public List<string> DialogueTextList = new List<string>();
    public int dialogueNumberToPlay = 0;

    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
        audioSource = soundPlayer.gameObject.GetComponent<AudioSource>();
        activation = GetComponent<Activation>();
    }

    void Update()
    {
        if (activation.activated && !activated)
        {
            if (IntroOutro)
            {
                StartCoroutine(VoiceoverIntro());
            }
            else
            {
                StartCoroutine(ActivateVoiceover());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            if (IntroOutro)
            {
                StartCoroutine(VoiceoverIntro());
            }
            else
            {
                StartCoroutine(ActivateVoiceover());
            }

        }
    }

    public IEnumerator VoiceoverIntro()
    {
        activation.activated = false;
        activated = true;
        audioSource.PlayOneShot(soundPlayer.audioMessageIntro);
        yield return new WaitWhile(() => audioSource.isPlaying);
        StartCoroutine(ActivateVoiceover());

    }

    public IEnumerator ActivateVoiceover()
    {
        activation.activated = false;
        activated = true;
        GameObject subtitles = GameObject.Find("Subtitles");


        subtitles.GetComponent<Animator>().SetBool("Visible", true);
        subtitles.GetComponent<Text>().text = DialogueTextList[dialogueNumberToPlay].ToString();
        audioSource.PlayOneShot(DialogueClipList[dialogueNumberToPlay]);

        yield return new WaitWhile(() => audioSource.isPlaying);



        //End
        dialogueNumberToPlay += 1;

        if (dialogueNumberToPlay < DialogueClipList.Count)
        {
            StartCoroutine(ActivateVoiceover());
        }
        else
        {
            dialogueNumberToPlay = 0;

            if (IntroOutro)
            {
                audioSource.PlayOneShot(soundPlayer.audioMessageOutro);
            }

            subtitles.GetComponent<Animator>().SetBool("Visible", false);

            activation.activated = false;

            if (!oneTimePlay)
            {
                activated = false;
            }
        }
    }
    



    /*
    public IEnumerator ActivateVoiceover()
    {

        activation.activated = false;
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


        if (dialogueToPlay != null)
        {
            subtitles.GetComponent<Animator>().SetBool("Visible", true);
            subtitles.GetComponent<Text>().text = dialogueText2.ToString();
            audioSource.PlayOneShot(dialogueToPlay2);
            yield return new WaitWhile(() => audioSource.isPlaying);
        }




        if (IntroOutro)
        {
            audioSource.PlayOneShot(soundPlayer.audioMessageOutro);
        }

        subtitles.GetComponent<Animator>().SetBool("Visible", false);

        activation.activated = false;

        if (!oneTimePlay)
        {
            activated = false;
        }
    }
    */
}
