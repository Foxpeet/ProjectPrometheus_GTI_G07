using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMinijuegos : MonoBehaviour
{
    static System.Random random;
    List<GameObject> minijuegos;
    List<string> nombres;

    public GameObject cables;
    public GameObject laberinto;
    public GameObject codigo;

    public Abrision scriptPuerta;
    GameObject MinijuegoEjecutado;

    // Start is called before the first frame update
    void Start()
    {
        minijuegos = new List<GameObject>();
        minijuegos.Add(cables);
        //minijuegos.Add(laberinto);
        minijuegos.Add(codigo);

        nombres = new List<string>();
        nombres.Add("cables");
        //nombres.Add("laberinto");
        nombres.Add("codigo");

        random = new System.Random(Environment.TickCount);
    }

    public void MinijuegoAleatorio(GameObject zonaColisionPuerta)
    {

        var value = random.Next(0, minijuegos.Count);
        GameObject elegido = minijuegos[value];
        string nombreelegido = nombres[value];
        Debug.Log(value.ToString());
        LlamarMinijuego(zonaColisionPuerta, elegido, nombreelegido);
    }

    public void LlamarMinijuego(GameObject zonaColisionPuerta, GameObject elegido, string nombreElegido)
    {
        scriptPuerta = zonaColisionPuerta.GetComponent<Abrision>();

        MinijuegoEjecutado = Instantiate(elegido);
        switch (nombreElegido)
        {
            case "cables":
                MinijuegoCables minijuegoScriptCable = MinijuegoEjecutado.GetComponent<MinijuegoCables>();
                minijuegoScriptCable.controlador = this.gameObject;
                break;

            case "laberinto":
                MinijuegoLaberinto minijuegoScriptLaberinto = MinijuegoEjecutado.GetComponent<MinijuegoLaberinto>();
                minijuegoScriptLaberinto.controlador = this.gameObject;
                break;

            case "codigo":
                MinijuegoCodigo minijuegoScriptCodigo = MinijuegoEjecutado.GetComponent<MinijuegoCodigo>();
                minijuegoScriptCodigo.controlador = this.gameObject;
                break;

            default:
                break;
        }
    }

    public void Victoria()
    {
        scriptPuerta.minijuegoCompletado = true;
        scriptPuerta.llamarAnimacion();

        Cursor.lockState = CursorLockMode.Locked;

        Destroy(MinijuegoEjecutado, 0.2f);
    }
}
