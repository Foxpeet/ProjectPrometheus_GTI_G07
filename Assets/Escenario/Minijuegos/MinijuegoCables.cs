using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoCables : MonoBehaviour
{
    //lleva la cuenta de cuantos cables se han puesto correctamente
    public int conexionesActuales;

    //el objeto al que se va a poder afectar cuando se gane el minijuego (puertas)
    public GameObject llamador;

    public void ComprobarVictoria()
    {
        //si los 4 cables estan bien conectados:
        if (conexionesActuales == 4)
        {
            //para devolver la victoria ( en este caso cambiarle el color al objeto desde el cual se ha instanciado el minijuego)
            llamador.GetComponent<Renderer>().material.color = new Color(0, 255, 0);

            //destruye el gameobject del minijuego automaticamnete
            Destroy(this.gameObject, 1f);
        }
    }
}
