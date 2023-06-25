using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Flame2 : MonoBehaviour
{
    public VisualEffect[] flameSpawners;
    private RaycastHit hit;
    public GameObject trap;
    private bool isTrapOn = false;
    public  int colCount;
    public int nrOfEntries;
    public LayerMask layerMask;
    private GameObject c;
    public GameObject col;
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
        isTrapOn = false;
        flameSpawners = gameObject.GetComponentsInChildren<VisualEffect>();
        foreach (VisualEffect effect in flameSpawners)
        {
            effect.Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTrapOn)
        {
            
            
            if (Physics.BoxCast(trap.transform.position , new Vector3(1f,-2f,1f),
                    Vector3.up,out hit,Quaternion.identity,10f,layerMask:layerMask ))
            {
                
               hit.transform.gameObject.GetComponent<MovementAgent>().Lose();
                
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (colCount == nrOfEntries)
        {
            isTrapOn = true;
            //c = Instantiate(col, new Vector3(this.transform.position.x,this.transform.position.y+1,this.transform.position.z), Quaternion.identity);
            foreach (VisualEffect effect in flameSpawners)
            {
                effect.Play();
                Invoke("BoolChange", 1.5f);
            }
        }
        colCount++;
    }


    public void BoolChange()
    {
        if (c != null)
        {
            Destroy(c);
        }
        isTrapOn = false;
        foreach (VisualEffect effect in flameSpawners)
        {
            effect.Stop();
        }

        
        trap.tag= "Trap26Inactive";
    }

}
