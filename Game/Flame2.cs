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
    public ReturnMenu winMenu;
    
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
            
            if (Physics.BoxCast(trap.transform.position , new Vector3(1f,-2f,1f),
                    Vector3.up,out hit,Quaternion.identity,10f,layerMask:layerMask ))
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

        
        this.tag = "Trap26Inactive";
    }

    public void Play()
    {
        isTrapOn = true;
    }

}
