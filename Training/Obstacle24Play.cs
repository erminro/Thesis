using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle24Play : MonoBehaviour
{
    public int nrOfEntries;

    public int colCount;
    // Start is called before the first frame update
    void Start()
    {

        int prob = Random.Range(0, 30);
        if (prob < 10)
        {
            nrOfEntries = 0;
        }else if (prob < 20)
        {
            nrOfEntries = 1;
        }else 
        {
            nrOfEntries = 2;
        }
        colCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (colCount == nrOfEntries)
        {
            this.tag = "Trap24Inactive";
            transform.GetChild(0).GetComponent<Animator>().Play("Obstacle24Anime");
        }

        colCount++;
    }
}
