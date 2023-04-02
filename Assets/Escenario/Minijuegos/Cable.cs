using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Cable : MonoBehaviour
{

    //el sprite/imagen de la parte del cable que se va a estirar cuando arrastremos
    public SpriteRenderer finalCable;
    //la luz que se encendera en verde cuando conectemos bien el cable
    public GameObject luz;

    //la posicion y rotacion originales del cable para poder resetear el cable cuando soltamos el click y no se ha conectado correctamente
    private Vector3 posicionOriginal;
    private Vector3 tamanoOriginal;

    //para guardar el script al que accederemos para comprobar cuando gana el minijuego
    private MinijuegoCables minijuegoCables;

    void Start()
    {
        //guardamos la posicion y rotacion originales para poder reiniciar el cable
        posicionOriginal = transform.position;
        tamanoOriginal = finalCable.size;
        //y guardamos el script para comprobar cuando gana y llevar cuenta de los cables bien conectados
        minijuegoCables = transform.root.gameObject.GetComponent<MinijuegoCables>();
    }

    void Update()
    {
        //si el jugador deja de pulsar el click izq: se reinicia el cable
        if (Input.GetMouseButtonUp(0))
        {
            Reiniciar();
        }
    }

    //cuando el jugador arrastra con el click un cable:
    private void OnMouseDrag()
    {
        ActualizarPosicion();
        ComprobarConexion();
        ActualizarRotacion();
        ActualizarTama�o();
    }

    //para poder mover el cable a donde este el raton del jugador
    private void ActualizarPosicion()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        transform.position = mousePosition;
    }
    
    //para rotar y hacer que el cable tenga angulo mientras lo movemos por la pantalla
    private void ActualizarRotacion()
    {
        Vector3 posicionActual = transform.position;
        Vector3 puntoOrigen = transform.parent.position;
        //hemos guardado el origen del cable y donde esta en cada momento para calcular la direccion y angulo en el que esta el raton respecto el inicio del cable
        Vector3 direccion = posicionActual - puntoOrigen;
        Vector3 from = (Vector3.right * transform.lossyScale.x);

        float angulo = Vector3.SignedAngle(from, direccion, Vector3.forward);

        //asignar la rotacion calculada al cable
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
    
    private void ActualizarTama�o()
    {
        //guardamos posicion origen y del raton para calcular la distancia entre ellos y saber cuanto estirar la imagen del cable y parezca que se estira el cable en s�
        Vector3 posicionActual = transform.position;
        Vector3 puntoOrigen = transform.parent.position;

        float distancia = Vector3.Distance(posicionActual, puntoOrigen);

        //lo multiplicamos por 11.3f (f de float) porque no hice bien la escala y asi esta reajustado
        finalCable.size = new Vector3(distancia*11.3f, finalCable.size.y, 1);
    }
    
    //cuando soltamos el click de arrastrar un cable y no lo hemos conectado bien, se reinicia el cable a donde estaba al principio
    private void Reiniciar()
    {
        transform.position = posicionOriginal;
        transform.rotation = Quaternion.identity;
        finalCable.size = tamanoOriginal;
    }

    private void ComprobarConexion()
    {
        //una lista que contendra todos los cables a una distancia 0.02f del cable que estamos arrastrando
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.02f);

        foreach (Collider2D col in colliders)
        {
            // No procesamos el collider del cable que estamos moviendo.
            if (col.gameObject != gameObject)
            {
                //cuando detecte un cable que no sea el que estamos arrastrando se movera directamente a su posicion, aun si no es el correcto
                transform.position = col.transform.position;

                Cable otroCable = col.gameObject.GetComponent<Cable>();

                //si los colores de los cables coinciden
                if (finalCable.color == otroCable.finalCable.color)
                {

                    //La conexion es correcta y se eliminan los scripts de ambos cables (los dos que se han conectado) para no poder arrastrarlos mas
                    Conectar();
                    otroCable.Conectar();

                    //y guardamos que se ha conectado un cable mas correctamente
                    minijuegoCables.conexionesActuales++;
                    //y comprobamos si ya estan los 4 cables conectado, si es as� se acabara el minijuego
                    minijuegoCables.ComprobarVictoria();
                }
            }
        }
    }

    //cuando don cables correctos se conectan
    public void Conectar()
    {
        //se le enciende la luz verde
        luz.SetActive(true);
        //se le elimina el script para que no se le pueda arrastrar mas
        Destroy(this);
    }
}