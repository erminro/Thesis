using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool pressed;

    public Material green;
    public Material defaultMaterial;
    public MovementAgent movementAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(pressed)
            return;
        pressed = true;
        this.tag = "PressedPressurePlate";
        GetComponent<Renderer>().material = green;
        if (collision.transform.name == "Albert")
        {
            collision.gameObject.GetComponent<MovementAgent>().PressurePlatePress();
        }
    }

    public bool isPressed()
    {
        return pressed;
    }

    public void ResetMaterial()
    {
        this.tag = "PressurePlate";
        GetComponent<Renderer>().material = defaultMaterial;
        pressed = false;
    }
}
