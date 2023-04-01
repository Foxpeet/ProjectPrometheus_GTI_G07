using System;
using System.Collections;
using UnityEngine;

public class LlamarMinijuego : MonoBehaviour
{
    public GameObject Minijuego;
    public bool estado;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hola");
    }

    void OnMouseDown()
    {
        //destroy script
        //Destroy(this);

        //destroy object (este, el que tiene el script)
        //Destroy(gameObject);

        //change color of object
        //GetComponent<Renderer>().material.color = new Color(0, 0, 0); 0<x>255

        //Instantiate(Minijuego);

        if (isTaskActive())
        {
            Debug.Log("True");
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
        else
        {
            Debug.Log("False");
            Instantiate(Minijuego);
            GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
    }

    private bool isTaskActive()
    {
        return GameObject.FindWithTag("Minijuego");
    }
}
