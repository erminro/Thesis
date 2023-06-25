using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle8Play : MonoBehaviour
{
    public  int colCount;

    public int nrOfEntries;
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
            this.tag = "Trap8Inactive";
            transform.GetComponent<Animator>().Play("Obstacle8Anime");
        }
        colCount++;
    }
    
}
