using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPopUp : MonoBehaviour
{
    PauseMenu pauseMenu;

    public GameObject journalObject;
    public bool active;

    public float cooldownTime = 1;
    float cooldownTimeCountdown = 0;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && active)
        {
            DisablePopup();
        }

        if (cooldownTimeCountdown > 0)
        {
            cooldownTimeCountdown -= Time.deltaTime;
        }
    }

    public void ActivatePopUp()
    {
        active = true;
        cooldownTimeCountdown = cooldownTime;
        journalObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void DisablePopup()
    {
        active = false;
        journalObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
