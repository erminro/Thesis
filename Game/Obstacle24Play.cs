using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle24Play : MonoBehaviour
{
    public int nrOfEntries;
    public bool isTrapOn;
    public int colCount;
    // Start is called before the first frame update
    void Start()
    {

        isTrapOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrapOn)
        {
            this.tag = "Trap24Inactive";
            transform.GetChild(0).GetComponent<Animator>().Play("Obstacle24Anime");
            isTrapOn = false;
        }
    }



    public void Play()
    {
        isTrapOn = true;
    }
}
