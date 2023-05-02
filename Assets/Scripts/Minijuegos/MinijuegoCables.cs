using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoCables : MonoBehaviour
{
    //lleva la cuenta de cuantos cables se han puesto correctamente
    public int conexionesActuales;
    public Canvas canvass;

    //el objeto al que se va a poder afectar cuando se gane el minijuego (puertas)
    public GameObject llamador;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        canvass.renderMode = RenderMode.ScreenSpaceCamera;
        canvass.worldCamera = Camera.main;
    }
    public void ComprobarVictoria()
    {
        //si los 4 cables estan bien conectados:
        if (conexionesActuales == 4)
        {
            //para devolver la victoria ( en este caso cambiarle el color al objeto desde el cual se ha instanciado el minijuego)
            Abrision puertaScript = llamador.GetComponent<Abrision>();
            puertaScript.minijuegoCompletado = true;
            puertaScript.llamarAnimacion();

            Cursor.lockState = CursorLockMode.Locked;

            //destruye el gameobject del minijuego automaticamnete
            Destroy(this.gameObject, 1f);
        }
    }
}
