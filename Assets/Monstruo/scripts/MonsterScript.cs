using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    // RANGO DEL MONSTRUO
    private float monsterRangeHit = 2;
    //RANGO DETECCION JUGADOR
    private float playerRangeDetection = 3;
    // SPAWNS
    public Transform spawnMonstruo, spawnJugador;
    // VARIABLES DEL JUGADOR
    [SerializeField] GameObject player;
    CharacterController characterController;
    AgenteNav agenteNav;
    // TIEMPO DE ESPERA ENTRE REINICIOS
    private float time = 3;
    // TEST
    private bool oneTimeCheck = true;
    private bool onGameReset = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        agenteNav = gameObject.GetComponent<AgenteNav>();
    }

    // Update is called once per frame
    void Update()
    {
        // CONOCER SOBRE QUE SUELO SE ENCUENTRA EL JUGADOR
        Vector3 playerLocation = Vector3.down;
        Ray playerRay = new Ray(player.transform.position, player.transform.TransformDirection(playerLocation * playerRangeDetection));
        Debug.DrawRay(player.transform.position, player.transform.TransformDirection(playerLocation * playerRangeDetection));

        // DETECTAR SI EL MONSTRUO HA TOCADO AL JUGADOR
        Vector3 direccion = Vector3.forward;
        Ray rayoDeteccion = new Ray(transform.position, transform.TransformDirection(direccion * monsterRangeHit));
        Debug.DrawRay(transform.position, transform.TransformDirection(direccion * monsterRangeHit));
        

        // CUANDO EL JUGADOR ENTRA EN UNA SAFEZONE: 
        if(Physics.Raycast(playerRay, out RaycastHit playerAt, playerRangeDetection))
        {
            if (playerAt.collider.CompareTag("SafeZone") && oneTimeCheck == true && onGameReset == false)
            {
                Debug.Log("Player at SafeZone");
                agenteNav.patrol();
                oneTimeCheck = false;
            }
            if (!playerAt.collider.CompareTag("SafeZone") && oneTimeCheck == false && onGameReset == false)
            {
                agenteNav.chase();
                oneTimeCheck = true;
            }
        }
        
        // CUANDO EL MONSTRUO TOCA EL JUGADOR: 
        if (Physics.Raycast(rayoDeteccion, out RaycastHit monsterHit, monsterRangeHit))
        {
            if (monsterHit.collider.CompareTag("Player") && onGameReset == false)
            {
                // REINICIAR Y REALIZAR LA PAUSA
                StartCoroutine("gameReset");
            }
        }
    }

    IEnumerator gameReset()
    {
        onGameReset = true;
        // DESABILITAR LOS COMPONENTS PARA REALIZAR CORRECTAMENTE EL TELETRANSPORTE AL ORIGEN
        characterController.enabled = false;
        agenteNav.disable();

        // TELETRANSPORTE
        player.transform.position = spawnJugador.position;
        gameObject.transform.position = spawnMonstruo.position;

        // DESABILITAR EL NavMeshAgent DE AgentNav PARA EVITAR ERRORES
        agenteNav.time = time;

        // PAUSA Y REINICIAR TODO
        yield return new WaitForSeconds(time);
        onGameReset = false;
        characterController.enabled = true;
        agenteNav.enable();
    }
}
