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
    public ReturnMenu winMenu;
    private bool alberthit;
    // Start is called before the first frame update
    void Start()
    {
        
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
            foreach (VisualEffect effect in flameSpawners)
            {
                effect.Play();
                Invoke("BoolChange", 1.5f);
            }
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
                winMenu = GameObject.FindObjectOfType<ReturnMenu>();
                winMenu.SetWinMenuActive();
                Time.timeScale = 0;
                //hit.transform.gameObject.GetComponent<MovementAgent>().Lose();
            }
        }
 
    }



    public void BoolChange()
    {
        isTrapOn = false;
        foreach (VisualEffect effect in flameSpawners)
        {
            effect.Stop();
        }

        this.tag = "Trap28Inactive";
    }

    public void Play()
    {
        isTrapOn = true;
    }
}
