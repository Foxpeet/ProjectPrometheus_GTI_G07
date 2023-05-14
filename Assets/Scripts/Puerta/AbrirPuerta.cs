using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public Animator puerta;
    public GameObject Minijuego;

    public bool minijuegoCompletado;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !minijuegoCompletado)
        {
            //ControladorMinijuegos controlador = ControladorMinijuego.GetComponent<ControladorMinijuegos>();
            //controlador.MinijuegoAleatorio(this.gameObject);
        }
        else if (other.tag == "Player" && minijuegoCompletado)
        {
            puerta.SetBool("abrir", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.SetBool("abrir", false);
        }
    }

    public void llamarAnimacion()
    {
        puerta.SetBool("abrir", true);
    }
}
