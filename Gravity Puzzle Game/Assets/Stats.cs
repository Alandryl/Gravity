using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int gravityCoresCollected = 0;


    GameObject gravityCorePopUp;
    Text gravityCorePopUpText;

    void Start()
    {
        gravityCorePopUp = GameObject.Find("GravityCorePopUp");
        gravityCorePopUpText = GameObject.Find("CollectedGravityCoresPopUp").GetComponent<Text>();
        gravityCorePopUp.SetActive(false);
    }

    void Update()
    {
        
    }

    public void AddGravityCore()
    {
        StartCoroutine(AddGravityCoreAction());
    }

    public IEnumerator AddGravityCoreAction()
    {
        gravityCorePopUp.SetActive(true);
        gravityCorePopUp.GetComponent<Animator>().SetTrigger("PopUp");

        string gravityCoresCollectedString = gravityCoresCollected.ToString();
        gravityCorePopUpText.text = gravityCoresCollectedString;
        gravityCoresCollected += 1;

        yield return new WaitForSeconds(1.5f);

        gravityCoresCollectedString = gravityCoresCollected.ToString();
        gravityCorePopUpText.text = gravityCoresCollectedString;


        yield return new WaitForSeconds(2.75f);

        gravityCorePopUp.SetActive(false);


    }
}
