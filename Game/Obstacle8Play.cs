using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle8Play : MonoBehaviour
{
    public  int colCount;

    public int nrOfEntries;

    public bool isTrapOn;
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
            this.tag = "Trap8Inactive";
            transform.GetComponent<Animator>().Play("Obstacle8Anime");
            isTrapOn = false;
        }
        
    }
    


    public void Play()
    {
        isTrapOn = true;
    }

}
