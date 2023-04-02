using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public Animator puerta;
    
    private void OnTriggerEnter(Collider other){
        if(other.tag =="Player"){
            puerta.SetBool("abrir", true);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag =="Player"){
            puerta.SetBool("abrir",false);
        }
    }
}
