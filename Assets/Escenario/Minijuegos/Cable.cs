using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Cable : MonoBehaviour
{

    public SpriteRenderer finalCable;
    public GameObject luz;

    private Vector3 posicionOriginal;
    private Vector3 tamanoOriginal;
    //private TareaCables tareaCables;

    void Start()
    {
        posicionOriginal = transform.position;
        tamanoOriginal = finalCable.size;
        //tareaCables = transform.root.gameObject.GetComponent<TareaCables>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Reiniciar();
        }
    }

    private void OnMouseDrag()
    {
        ActualizarPosicion();
        ComprobarConexion();
        ActualizarRotacion();
        ActualizarTamaño();
    }

    private void ActualizarPosicion()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        transform.position = mousePosition;
    }
    
    private void ActualizarRotacion()
    {
        Vector3 posicionActual = transform.position;
        Vector3 puntoOrigen = transform.parent.position;

        Vector3 direccion = posicionActual - puntoOrigen;
        Vector3 from = (Vector3.right * transform.lossyScale.x);

        float angulo = Vector3.SignedAngle(from, direccion, Vector3.forward);

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
    
    private void ActualizarTamaño()
    {
        Vector3 posicionActual = transform.position;
        Vector3 puntoOrigen = transform.parent.position;

        float distancia = Vector3.Distance(posicionActual, puntoOrigen);

        finalCable.size = new Vector3(distancia*12.6f, finalCable.size.y, 1);
    }
    
    private void Reiniciar()
    {
        transform.position = posicionOriginal;
        transform.rotation = Quaternion.identity;
        finalCable.size = tamanoOriginal;
    }

    
    private void ComprobarConexion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.02f);

        foreach (Collider2D col in colliders)
        {
            // No procesamos el collider del cable que estamos moviendo.
            if (col.gameObject != gameObject)
            {
                transform.position = col.transform.position;

                Cable otroCable = col.gameObject.GetComponent<Cable>();

                if (finalCable.color == otroCable.finalCable.color)
                {

                    // Conexión correcta.
                    Conectar();
                    otroCable.Conectar();

                    //tareaCables.conexionesActuales++;
                    //tareaCables.ComprobarVictoria();
                }
            }
        }
    }

    public void Conectar()
    {
        luz.SetActive(true);
        Destroy(this);
    }
}