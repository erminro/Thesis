using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool trp8;
    public bool trp24;
    public bool trp26;
    public bool trp28;
    public List<PressurePlate> PressurePlates= new List<PressurePlate>();
    public List<GameObject> ParentPressurePlates = new List<GameObject>();
    public MovementAgent movementAgent;

    public Vector3 positionD;
    // Start is called before the first frame update
    private bool isok=false;
    
    void Start()
    {
        trp8 = true;
        trp24 = true;
        trp26 = true;
        trp28 = true;
        positionD = this.transform.position;
        GetComponent<Renderer>().material = red;
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
    public int CheckSpawnT1(float x , float z)
    {
        x = positionD.x - x;
        z = positionD.z - z;
        
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

        if (z < firstWall.transform.position.z + 1 + 0.25 && z > firstWall.transform.position.z - 1 - 0.25 &&
            x > firstWall.transform.position.x - 2 - 1.3f && x < firstWall.transform.position.x + 2 + 1.3f)
        {
            return 1;
        }
        if (z < secondWall.transform.position.z + 1 + 0.25 && z > secondWall.transform.position.z - 1 - 0.25 &&
            x > secondWall.transform.position.x - 2 - 1.3f && x < secondWall.transform.position.x + 2 + 1.3f)
        {
            return 1;
        }

        return 0;
    }
    public int CheckSpawnT3NoRotation(float x , float z)
    {
        x = positionD.x - x;
        z = positionD.z - z;
        
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
         x = positionD.x - x;
         z = positionD.z - z;
        
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
        x = positionD.x - x;
        z = positionD.z - z;
        
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

    public void ResetAll()
    {
        float x = Random.Range(-2.5f, 2.5f);
        float z = Random.Range(1.5f, 21.61f);
        if (ParentPressurePlates != null)
        {
            foreach (var pressurePlate in ParentPressurePlates)
            {
                Destroy(pressurePlate);
            }
        }

        trp8 = true;
        trp24 = true;
        trp26 = true;
        trp28 = true;
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
        
        int rtrap2=Random.Range(0, 4);
        int rtrap3=Random.Range(0, 2);
        int rtrap4=Random.Range(0, 4);
        
        
            x = Random.Range(-2.5f, 2.5f);
            z = Random.Range(1.5f, 21.61f);

            while (CheckSpawnT1(x, z) == 1)
            {
                x = Random.Range(-2.5f, 2.5f);
                z = Random.Range(1.5f, 21.61f);
            }


            GameObject t1gameObject = Instantiate(trap1, positionD - new Vector3(x, 1.4999f, z), Quaternion.identity);
            ParentPressurePlates.Add(t1gameObject);

            
            x = Random.Range(-2.5f, 2.5f);
            z = Random.Range(1.5f, 21.61f);




            while (CheckSpawn(x, z) == 1)
            {
                x = Random.Range(-2.5f, 2.5f);
                z = Random.Range(1.5f, 21.61f);
            }


            GameObject t2gameObject = Instantiate(trap2, positionD - new Vector3(x, 1.4999f, z), Quaternion.identity);
            t2gameObject.transform.Rotate(Vector3.up, 90 * rtrap2);
            ParentPressurePlates.Add(t2gameObject);
            
                x = Random.Range(-2.5f, 2.5f);
            z = Random.Range(1.5f, 21.61f);
            if (rtrap3 == 0)
            {
                while (CheckSpawnT3NoRotation(x, z) == 1)
                {
                    x = Random.Range(-2.5f, 2.5f);
                    z = Random.Range(1.5f, 21.61f);
                }
            }
            else
            {
                while (CheckSpawnT3Rotation(x, z) == 1)
                {
                    x = Random.Range(-2.5f, 2.5f);
                    z = Random.Range(1.5f, 21.61f);
                }
            }

            GameObject t3gameObject = Instantiate(trap3, positionD - new Vector3(x, 1.4999f, z), Quaternion.identity);
            t3gameObject.transform.Rotate(Vector3.up, 90 * rtrap3);
            ParentPressurePlates.Add(t3gameObject);
            
        
            x = Random.Range(-2.5f, 2.5f);
            z = Random.Range(1.5f, 21.61f);




            while (CheckSpawnT4(x, z) == 1)
            {
                x = Random.Range(-2.5f, 2.5f);
                z = Random.Range(1.5f, 21.61f);

            }
    

            GameObject t4gameObject = Instantiate(trap4, positionD - new Vector3(x, 1.4999f, z), Quaternion.identity);
            t4gameObject.transform.Rotate(Vector3.up, 90 * rtrap4);
            ParentPressurePlates.Add(t4gameObject);
        
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
        
        foreach (var tr in ParentPressurePlates)
        {
            if (tr.transform.tag == "Trap8Inactive" && trp8==true)
            {
                movementAgent.AddReward(0.25f);
                Debug.Log(movementAgent.GetCumulativeReward());
                trp8 = false;
            }

            if (tr.transform.tag == "Trap24Inactive" && trp24 == true)
            {
                movementAgent.AddReward(0.25f);
                Debug.Log(movementAgent.GetCumulativeReward());
                trp24 = false;
            }

            
            if (tr.tag == "Trap26Inactive" && trp26 == true)
            {
                movementAgent.AddReward(0.25f);
                Debug.Log(movementAgent.GetCumulativeReward());
                trp26 = false;
            }
            

            if (tr.tag == "Trap28Inactive" && trp28 == true)
            {
                movementAgent.AddReward(0.25f);
                Debug.Log(movementAgent.GetCumulativeReward());
                trp28 = false;
            }
            
        }
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
            movementAgent.Win();
            Debug.Log("You win");
        }
    }
}
