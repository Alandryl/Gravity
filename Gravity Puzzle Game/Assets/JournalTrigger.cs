using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalTrigger : MonoBehaviour
{
    public Sprite journalSprite;
    JournalPopUp journalPopUp;
    PauseMenu pauseMenu;

    void Start()
    {
        journalPopUp = FindObjectOfType<JournalPopUp>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            journalPopUp.journalObject.GetComponent<Image>().sprite = journalSprite;
            journalPopUp.ActivatePopUp();

            pauseMenu.journalSprites.Add(journalSprite);
            pauseMenu.unlockedJournalPages += 1;
            pauseMenu.currentJournalPage = pauseMenu.unlockedJournalPages;
            pauseMenu.journalEntryImage.sprite = pauseMenu.journalSprites[pauseMenu.currentJournalPage];

            Destroy(this);
        }
    }
}