using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoCodigo : MonoBehaviour
{
    public Canvas canvass;

    public GameObject controlador;
    ControladorMinijuegos controladorScript;

    void Start()
    {
        controladorScript = controlador.GetComponent<ControladorMinijuegos>();

        Cursor.lockState = CursorLockMode.Confined;
        canvass.renderMode = RenderMode.ScreenSpaceCamera;
        canvass.worldCamera = Camera.main;
        canvass.planeDistance = 1f;
    }
    public void EjecutarVictoria()
    {
        controladorScript.Victoria();
    }
}
