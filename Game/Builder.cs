using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Builder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] traps;
    public GameObject pendingTrap;
    public Vector3 pos;
    public RaycastHit hit;
    public GameObject backWall;
    public GameObject frontWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject firstWall;
    public GameObject secondWall;
    [SerializeField] private LayerMask layerMask;
    public float gridSize;
    public bool[] buttonsBool;
    public bool[] buttonsBool2;
    public Button[] buttons;
    public bool canplace;
    public Material[] materials;
    private int rotationt2;
    public Button start;
    public TMP_Text starttxt;
    public GameObject startButton;
    public bool startButtonbool;
    public TMP_Text timer;
    public bool starttimer;
    private float timeleft = 30;
    public GameObject Albert;
    private Obstacle8Play trap1;
    private Obstacle24Play trap2;
    private Flame2 trap3;
    private Flame1 trap4;
    public ReturnMenu winMenu;
    void Start()
    {
        
        start.interactable = false;
        startButtonbool = false;
        starttxt.color=Color.red;
        starttimer = false;
        canplace = true;
        for(int i=0;i<buttonsBool2.Length;i++)
        {
            buttonsBool2[i] = false;
        }
        for(int i=0;i<buttonsBool.Length;i++)
        {
            buttonsBool[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingTrap == null)
        {
            if (buttonsBool[0] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SelectObject(0);
                    buttonsBool[0] = true;
                }
            }

            if (buttonsBool[1] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SelectObject(1);
                    buttonsBool[1] = true;
                }
            }

            if (buttonsBool[2] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    SelectObject(2);
                    buttonsBool[2] = true;
                }
            }

            if (buttonsBool[3] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    SelectObject(3);
                    buttonsBool[3] = true;
                }
            }
        }

        if (pendingTrap != null)
        {
            MaterialUpdate();
            pendingTrap.transform.position = new Vector3(RoundGrid(pos.x), RoundGrid(pos.y), RoundGrid(pos.z));
            pendingTrap.transform.position = pos;
            if (pendingTrap.transform.CompareTag(traps[0].transform.tag))
            {
    
                if (CheckSpawnT4(pos.x, pos.z) == 1)
                {
                    canplace = false;
                    MaterialUpdate();
                }
                else
                {
                    canplace = true;
                    MaterialUpdate();
                }
            }
            if (pendingTrap.transform.CompareTag(traps[1].transform.tag))
            {
                if (rotationt2 == 0)
                {
                    if (CheckSpawnT3NoRotation(pos.x, pos.z) == 1)
                    {
                        canplace = false;
                        MaterialUpdate();
                    }
                    else
                    {
                        canplace = true;
                        MaterialUpdate();
                    }
                }
                else
                {
                    if (CheckSpawnT3Rotation(pos.x, pos.z) == 1)
                    {
                        canplace = false;
                        MaterialUpdate();
                    }
                    else
                    {
                        canplace = true;
                        MaterialUpdate();
                    }
                }
            }
            if (pendingTrap.transform.CompareTag(traps[2].transform.tag))
            {
    
                if (CheckSpawnT1(pos.x, pos.z) == 1)
                {
                    canplace = false;
                    MaterialUpdate();
                }
                else
                {
                    canplace = true;
                    MaterialUpdate();
                }
            }
            if (pendingTrap.transform.CompareTag(traps[3].transform.tag))
            {
    
                if (CheckSpawn(pos.x, pos.z) == 1)
                {
                    canplace = false;
                    MaterialUpdate();
                }
                else
                {
                    canplace = true;
                    MaterialUpdate();
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (canplace)
                {
                    MeshRenderer[] chd = pendingTrap.GetComponentsInChildren<MeshRenderer>();
                    foreach (var msh in chd)
                    {
                        msh.material = materials[2];
                    }

                    PlaceObject();
                }

            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObj();
            }
        }

        if (buttonsBool[0] && buttonsBool[1] && buttonsBool[2] && buttonsBool[3] && pendingTrap==null)
        {

            starttxt.color= Color.green;
            start.interactable = true;
        }

        if (startButtonbool)
        {
            foreach (var but in buttons)
            {
                but.interactable = true;
            }

            trap1 = FindObjectOfType<Obstacle8Play>();
            Debug.Log(trap1);
            trap2 = FindObjectOfType<Obstacle24Play>();
            trap3 = FindObjectOfType<Flame2>();
            trap4 = FindObjectOfType<Flame1>();
            starttimer = true;
            
            startButtonbool = false;
            Destroy(startButton);
        }
        if (starttimer == true)
        {
            if (buttonsBool2[0] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    trap1.GetComponent<Obstacle8Play>().Play();
                    buttonsBool2[0] = true;
                    buttons[0].interactable = false;
                }
            }
            if (buttonsBool2[1] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    trap2.GetComponent<Obstacle24Play>().Play();
                    buttonsBool2[1] = true;
                    buttons[1].interactable = false;
                }
            }
            if (buttonsBool2[2] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    trap3.GetComponent<Flame2>().Play();
                    buttonsBool2[2] = true;
                    buttons[2].interactable = false;
                }
            }
            if (buttonsBool2[3] == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    trap4.GetComponent<Flame1>().Play();
                    buttonsBool2[3] = true;
                    buttons[3].interactable = false;
                }
            }
            if (timeleft > 0)
            {
                timeleft = timeleft - Time.deltaTime;
                UpdateTime(timeleft);
            }
            else
            {
                winMenu = GameObject.FindObjectOfType<ReturnMenu>();
                winMenu.SetWinMenuActive();
                Time.timeScale = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void PlaceObject()
    {
        pendingTrap = null;
    }

    public void SelectObject(int indx)
    {
        pendingTrap = Instantiate(traps[indx], pos, transform.rotation);
        buttonsBool[indx] = true;
        buttons[indx].interactable = false;
    }

    public float RoundGrid(float pos)
    {
        float diff = pos % gridSize;
        pos -= diff;
        if (diff > (gridSize / 2))
        {
            pos = pos + gridSize;
        }

        return pos;
    }

    public void RotateObj()
    {
        pendingTrap.transform.Rotate(Vector3.up,90);
        if (pendingTrap.transform.tag == traps[1].transform.tag)
        {
            rotationt2++;
            if (rotationt2 == 2)
            {
                rotationt2 = 0;
            }
        }
    }

    public void MaterialUpdate()
    {
       
            MeshRenderer[] chd = pendingTrap.GetComponentsInChildren<MeshRenderer>();
            foreach (var msh in chd)
            {
                if (canplace == true)
                {
                    msh.material = materials[0];
                }
                else
                {
                    msh.material = materials[1];
                }
            }

    }
        public int CheckSpawn(float x , float z)
    {
        
        if (x < backWall.transform.position.x + 0.25 + 0.5)
        {
            return 1;
        }

        if (x > frontWall.transform.position.x - 0.25 - 0.5)
        {
            return 1;
        }

        if (z < leftWall.transform.position.z + 3.13)
        {
            return 1;
        }

        if (z > rightWall.transform.position.z - 1)
        {
            return 1;
        }

        if (z < firstWall.transform.position.z + 0.5 + 0.25 && z > firstWall.transform.position.z - 0.5 - 0.25 &&
            x > firstWall.transform.position.x - 2 - 0.5 && x < firstWall.transform.position.x + 2 + 0.5)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 0.5 + 0.25 && z > secondWall.transform.position.z - 0.5 - 0.25 &&
            x > secondWall.transform.position.x - 2 - 0.5 && x < secondWall.transform.position.x + 2 + 0.5)
        {
            return 1;
        }

        return 0;
    }
    public int CheckSpawnT1(float x , float z)
    {
        
        if (x < backWall.transform.position.x + 1.3f)
        {
            return 1;
        }

        if (x > frontWall.transform.position.x - 1)
        {
            return 1;
        }

        if (z < leftWall.transform.position.z + 4.13)
        {
            return 1;
        }

        if (z > rightWall.transform.position.z - 2)
        {
            return 1;
        }

        if (z < firstWall.transform.position.z + 1.3f + 0.25 && z > firstWall.transform.position.z - 1.3f - 0.25 &&
            x > firstWall.transform.position.x - 2 - 1.3f && x < firstWall.transform.position.x + 2 + 1.3f)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 1.3f + 0.25 && z > secondWall.transform.position.z - 1.3f - 0.25 &&
            x > secondWall.transform.position.x - 2 - 1.3f && x < secondWall.transform.position.x + 2 + 1.3f)
        {
            return 1;
        }

        return 0;
    }

    public void NextButton()
    {
        startButtonbool = true;
        Instantiate(Albert, new Vector3(0, 5.5f, -1.78f), Quaternion.identity);
    }
    public int CheckSpawnT3NoRotation(float x , float z)
    {

        if (x < backWall.transform.position.x + 1.6f)
        {
            return 1;
        }

        if (x > frontWall.transform.position.x - 1.5)
        {
            return 1;
        }

        if (z < leftWall.transform.position.z + 4.13)
        {
            return 1;
        }

        if (z > rightWall.transform.position.z - 2)
        {
            return 1;
        }

        if (z < firstWall.transform.position.z + 1 + 0.25 && z > firstWall.transform.position.z - 1 - 0.25 &&
            x > firstWall.transform.position.x - 2 - 1.6f && x < firstWall.transform.position.x + 2 + 1.6f)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 1 + 0.25 && z > secondWall.transform.position.z - 1 - 0.25 &&
            x > secondWall.transform.position.x - 2 - 1.6f && x < secondWall.transform.position.x + 2 + 1.6f)
        {
            return 1;
        }

        return 0;
    }
    public int CheckSpawnT3Rotation(float x , float z)
    {
        
        if (x < backWall.transform.position.x + 1f)
        {
            return 1;
        }

        if (x > frontWall.transform.position.x - 1.6)
        {
            return 1;
        }

        if (z < leftWall.transform.position.z + 4.13)
        {
            return 1;
        }

        if (z > rightWall.transform.position.z - 2)
        {
            return 1;
        }

        if (z < firstWall.transform.position.z + 1.6f + 0.25 && z > firstWall.transform.position.z - 1.6f - 0.25 &&
            x > firstWall.transform.position.x - 2 - 1 && x < firstWall.transform.position.x + 2 + 1)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 1.6f + 0.25 && z > secondWall.transform.position.z - 1.6f - 0.25 &&
            x > secondWall.transform.position.x - 2 - 1 && x < secondWall.transform.position.x + 2 + 1)
        {
            return 1;
        }

        return 0;
    }
    public int CheckSpawnT4(float x , float z)
    {

        if (x < backWall.transform.position.x + 0.25 + 1.25)
        {
            return 1;
        }

        if (x > frontWall.transform.position.x - 0.25 - 1.25)
        {
            return 1;
        }

        if (z < leftWall.transform.position.z + 4.13)
        {
            return 1;
        }

        if (z > rightWall.transform.position.z - 1.5)
        {
            return 1;
        }

        if (z < firstWall.transform.position.z + 1.25 + 0.25 && z > firstWall.transform.position.z - 1.25 - 0.25 &&
            x > firstWall.transform.position.x - 2 - 0.5 && x < firstWall.transform.position.x + 2 + 0.5)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 1.25 + 0.25 && z > secondWall.transform.position.z - 1.25 - 0.25 &&
            x > secondWall.transform.position.x - 2 - 1.25 && x < secondWall.transform.position.x + 2 + 1.25)
        {
            return 1;
        }

        return 0;
    }
    public void UpdateTime(float curentTime)
    {
        curentTime+=1;
        float secodns = Mathf.FloorToInt(curentTime % 60);
        timer.text = secodns.ToString();
    }
}
