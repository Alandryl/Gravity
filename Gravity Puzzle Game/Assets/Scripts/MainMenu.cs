using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    /*
    [Header("UI")]
    public GameObject black;
    public Animator blackAnim;
    */

    void Start ()
    {
        /*
        black = GameObject.Find("Black");
        blackAnim = black.GetComponent<Animator>();
        */
    }
	
	void Update ()
    {
		
	}

    public void Play ()
    {
        SceneManager.LoadScene("MainScene");
        //StartCoroutine(StartGame());
    }

    /*
    IEnumerator StartGame()
    {
        //blackAnim.SetBool("Fade", true);
        //yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }
    */

    public void Quit()
    {
        Application.Quit();
    }
}
