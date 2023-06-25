using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject Albert;
    public Button[] buttons;
    public bool buttonsfalse;
    public TMPro.TMP_Text timer;
    public bool starttimer;
    private float timeleft = 30;
    private void Start()
    {
        buttonsfalse = false;
        starttimer = false;
    }

    private void Update()
    {
        if (buttonsfalse == true)
        {
            Destroy(buttons[0].gameObject);
            buttonsfalse = false;
            starttimer = true;
        }

        if (starttimer == true)
        {
            if (timeleft > 0)
            {
                timeleft = timeleft - Time.deltaTime;
                UpdateTime(timeleft);
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    public void spawnAlbert()
    {
        buttonsfalse = true;
        //Time.timeScale = 0;
        Instantiate(Albert, new Vector3(0, 0.5f, -1.78f), Quaternion.identity);
        
    }
    public void UpdateTime(float curentTime)
    {
        curentTime+=1;
        float secodns = Mathf.FloorToInt(curentTime % 60);
        timer.text = secodns.ToString();
    }
}
