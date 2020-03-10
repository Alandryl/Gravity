using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject cube;
    Item itemScript;

    bool active = true;

    void Start()
    {
        itemScript = GetComponent<Item>();        
    }

    void Update()
    {
        if(itemScript.pickedUp == true)
        {
            cube.SetActive(false);
        }
        else
        {
            cube.SetActive(true);
        }

    }
}
