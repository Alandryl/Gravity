using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    Stats stats;

    public static bool gameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject pauseMenuMainUI;
    public GameObject HowToPlayUI;


    Text gravityCoresCollectedText;


    [Header("UI")]
    GameObject black;
    Animator blackAnim;

    [HideInInspector] public int gravityCoresCollected;
     


    void Awake()
    {
        stats = FindObjectOfType<Stats>();
        gravityCoresCollectedText = GameObject.Find("GravityCoresCollectedText").GetComponent<Text>();



        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        black = GameObject.Find("BlackScreen");
        blackAnim = black.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        gravityCoresCollected = stats.gravityCoresCollected;
        string gravityCoresCollectedString = gravityCoresCollected.ToString();

        gravityCoresCollectedText.text = gravityCoresCollectedString;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuMainUI.SetActive(true);
        HowToPlayUI.SetActive(false);

        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


}
