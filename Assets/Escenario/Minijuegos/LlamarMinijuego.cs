using System;
using System.Collections;
using UnityEngine;

public class LlamarMinijuego : MonoBehaviour
{
    public GameObject Minijuego;
    public bool estado;

    private GameObject MinijuegoNuevo;

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
            //el juego esta activo, no se crea otro minijuego ni nada
            Debug.Log("True");
        }
        else
        {
            //no habia minijuego
            Debug.Log("False");
            //se crea el minijuego
            MinijuegoNuevo = Instantiate(Minijuego);
            //se obtiene el gameObject del minijuego para pasarle ESTE objeto para cuando se complete el minijuego, éste pueda afectarlo, por ejemplo pueda abrir la puerta
            MinijuegoCables controlador = MinijuegoNuevo.GetComponent<MinijuegoCables>();
            controlador.llamador = this.gameObject;
        }
    }

    //funcion para comprobar si ya hay en escena un gameObject que tenga el tag de "Minijuego"
    private bool isTaskActive()
    {
        return GameObject.FindWithTag("Minijuego");
    }
}
