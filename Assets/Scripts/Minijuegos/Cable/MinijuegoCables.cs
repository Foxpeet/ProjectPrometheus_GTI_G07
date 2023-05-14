using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoCables : MonoBehaviour
{
    //lleva la cuenta de cuantos cables se han puesto correctamente
    public int conexionesActuales;
    public Canvas canvass;

    public GameObject controlador;
    ControladorMinijuegos controladorScript;

    void Start()
    {
        controladorScript = controlador.GetComponent<ControladorMinijuegos>();

        Cursor.lockState = CursorLockMode.Confined;
        canvass.renderMode = RenderMode.ScreenSpaceCamera;
        canvass.worldCamera = Camera.main;
    }
    public void ComprobarVictoria()
    {
        //si los 4 cables estan bien conectados:
        if (conexionesActuales == 4)
        {
            controladorScript.Victoria();
        }
    }
}
