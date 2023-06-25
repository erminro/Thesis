using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class GameLogic : MonoBehaviour
{

    public Material green;
    public Material red;
    public GameObject pressurePrefab;
    public GameObject backWall;
    public GameObject frontWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject firstWall;
    public GameObject secondWall;
    public GameObject trap1;
    public GameObject trap2;
    public GameObject trap3;
    public GameObject trap4;
    bool doorIsOpen = false;
    public GameObject loseMenu;
    public List<PressurePlate> PressurePlates= new List<PressurePlate>();
    public List<GameObject> ParentPressurePlates = new List<GameObject>();
    public MovementAgent movementAgent;

    public Vector3 positionD;
    // Start is called before the first frame update
    private bool isok=false;
    
    void Start()
    {
        positionD = this.transform.position;
        Debug.Log(positionD);
        GetComponent<Renderer>().material = red;
        float x = Random.Range(-2.5f, 2.5f);
        float z = Random.Range(1.5f, 21.61f);
        if (ParentPressurePlates != null)
        {
            foreach (var pressurePlate in ParentPressurePlates)
            {
                Destroy(pressurePlate);
            }
        }

        
        PressurePlates = new List<PressurePlate>();
        ParentPressurePlates = new List<GameObject>();
        int nrOfPlates = Random.Range(1, 4);

        for (int index = 0; index <= nrOfPlates; index++)
        {
            isok = false;

            if (ParentPressurePlates != null)
            {
               

                    while ( isok==false || CheckSpawn(x,z)==1  )
                    {
                         x = Random.Range(-2.5f, 2.5f);
                         z = Random.Range(1.5f, 21.61f);
                         isok = true;
                        foreach (var pressure in ParentPressurePlates)
                        {
                            
                            float distance = Vector3.Distance(pressure.transform.position,
                                positionD - new Vector3(x, 1.4999f, z));
                            
                            if (distance < 2f)
                            {
                                isok = false;
                                break;
                            }
                            
                        }

                    }
                
            }
            else
            {
                 x = Random.Range(-2.5f, 2.5f);
                 z = Random.Range(1.5f, 21.61f);
                while (CheckSpawn(x, z) == 1)
                {
                    x = Random.Range(-2.5f, 2.5f);
                    z = Random.Range(1.5f, 21.61f);
                }
            }

            


            GameObject ngameObject = Instantiate(pressurePrefab,positionD -  new Vector3(x, 1.49f, z), Quaternion.identity);
            ParentPressurePlates.Add(ngameObject);
            PressurePlates.Add(ngameObject.GetComponent<PressurePlate>());
        }
    }

    public int CheckSpawn(float x , float z)
    {
        x = positionD.x - x;
        z = positionD.z - z;
        
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


    public void ResetAll()
    {
        
       

            doorIsOpen = false;
        this.tag = "UnOpenedDoor";
        GetComponent<Renderer>().material = red;
        foreach (var pressurePlate in PressurePlates)
        {
            pressurePlate.ResetMaterial();
        }
    }

    // Update is called once per frame
    void Update()
    {
 
        bool notGreenTemp = false;
        foreach (var pressurePlate in PressurePlates)
        {
            if (!pressurePlate.isPressed())
            {
                notGreenTemp = true;
            }
    
        }

        if (notGreenTemp == false)
        {
            GetComponent<Renderer>().material = green;
            this.tag = "Door";
            doorIsOpen = true;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (doorIsOpen)
        {
            loseMenu.SetActive(true);
            Time.timeScale = 0;
            //movementAgent.Win();
            Debug.Log("You win");
        }
    }
}
