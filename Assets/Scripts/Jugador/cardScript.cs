using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class cardScript : MonoBehaviour
{
    private int layerMask_Card = 1 << 10;
    private int layerMask_Panel = 1 << 9;
    private RaycastHit raycastHit;
    private cameraRay cameraray;
    private bool hasCard = false;
    private bool interaccion = true;
    private bool puertaAbierta = false;

    public GameObject card; 
    public GameObject panelTexto;
    public GameObject zonaDeColision;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraray = gameObject.GetComponent<cameraRay>();
        panelTexto.SetActive(false);
        zonaDeColision.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(cameraray.ray, out raycastHit, cameraray.cameraRange, layerMask_Card))
        {
            text.text = "Presione E para recoger";
            panelTexto.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                card.SetActive(false);
                hasCard = true;
            }
        }
        else if (Physics.Raycast(cameraray.ray, out raycastHit, cameraray.cameraRange, layerMask_Panel))
        {
            panelTexto.SetActive(true);
            if (interaccion)
            {
                text.text = "Presione E para activar";
                interaccion = false;
            }
            else if (Input.GetKeyDown(KeyCode.E) && !hasCard) // NO TIENES LA TARJETA
            {
                text.text = "¡No dispones de carta!";
            }
            else if (Input.GetKeyDown(KeyCode.E) && hasCard) // TIENES LA TARJETA
            {
                text.text = "¡Puerta abierta!";
                puertaAbierta = true;
                zonaDeColision.SetActive(true);
            }
            else if (puertaAbierta)
            {
                text.text = "¡Puerta abierta!";
            }
        }
        else
        {
            panelTexto.SetActive(false);
            interaccion = true;
        }
    }
}