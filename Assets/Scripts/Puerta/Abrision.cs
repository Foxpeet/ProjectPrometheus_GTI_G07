using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrision : MonoBehaviour
{
    public LayerMask layerMask_Player;
    public LayerMask layerMask_Monster;

    public Animator puertaDer;
    public Animator puertaIz;

    public GameObject Minijuego;
    public bool minijuegoCompletado;

    private void OnTriggerEnter(Collider other){
        if (!minijuegoCompletado)
        {
            GameObject NewMinijuego = Instantiate(Minijuego);
            MinijuegoCables controlador = NewMinijuego.GetComponent<MinijuegoCables>();
            controlador.llamador = this.gameObject;
            return;
        }
        else
        {
            Debug.Log("asd");
            if ((layerMask_Player.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                Debug.Log("TEST");
                puertaDer.SetBool("PuertaDerActivity", true);
                puertaIz.SetBool("PuertaIzActivity", true);
            }
            if ((layerMask_Monster.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                Debug.Log("TEST");
                puertaDer.SetBool("PuertaDerActivity", true);
                puertaIz.SetBool("PuertaIzActivity", true);
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if(((layerMask_Player.value & (1 << other.transform.gameObject.layer)) > 0) || ((layerMask_Monster.value & (1 << other.transform.gameObject.layer)) > 0))
        {
            puertaDer.SetBool("PuertaDerActivity",false);
            puertaIz.SetBool("PuertaIzActivity", false);
        }
    }

    public void llamarAnimacion()
    {
        puertaDer.SetBool("PuertaDerActivity", true);
        puertaIz.SetBool("PuertaIzActivity", true);
    }
}
