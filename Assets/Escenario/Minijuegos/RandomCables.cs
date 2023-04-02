using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCables : MonoBehaviour
{

    //cada vez que se cree un minijuego nuevo
    void Awake()
    {
        //pasando por todos los cables de un lado, los va intercambiando para tener siempre los cables aleatorios
        for (int i = 0; i < transform.childCount; i++)
        {
            //guardamos los dos objetos de cables que vamos a intercambiar
            GameObject cableActual = transform.GetChild(i).gameObject;
            GameObject otroCable = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;

            //guardamos sus podiciones para poder intercambiarlos
            Vector3 nuevaPosCableActual = otroCable.transform.position;
            Vector3 nuevaPosOtroCable = cableActual.transform.position;

            //les asignamos a cada uno la posicion del otro
            cableActual.transform.position = nuevaPosCableActual;
            otroCable.transform.position = nuevaPosOtroCable;
        }
    }
}
