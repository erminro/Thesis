using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Flame1 : MonoBehaviour
{
    private bool isTrapOn=false;
    public VisualEffect[] flameSpawners;
    private RaycastHit hit;
    public GameObject trap;
    public int nrOfEntries;
    public int colCount;
    public LayerMask layerMask;

    private bool alberthit;
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
            Vector3 vector;
            Vector3 pos;
            Vector3 add;
            if (trap.transform.rotation.w < -0.7)
            {
                vector = Vector3.left;
                pos = new Vector3(-0.6f, 0, 0);
                add = new Vector3(0.5f, 0, 0);
            }
            else if(trap.transform.rotation.w==0)
            {
                vector = Vector3.back;
                pos = new Vector3(0, 0, -0.6f);
            }else if (trap.transform.rotation.w == 1f)
            {
                vector=Vector3.forward;
                pos = new Vector3(0, 0, 0.6f);
            }
            else
            {
                vector = Vector3.right;
                pos = new Vector3(0.6f, 0, 0);
                
            }
            if (Physics.BoxCast(trap.transform.position- pos , new Vector3(0.35f,2f,1f),
                    vector,out hit,trap.transform.rotation,2f,layerMask:layerMask ))
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
        isTrapOn = false;
        foreach (VisualEffect effect in flameSpawners)
        {
            effect.Stop();
        }

        trap.tag = "Trap28Inactive";
        trap.GetComponentInParent<Transform>().tag = "Trap28Inactive";
    }


}
