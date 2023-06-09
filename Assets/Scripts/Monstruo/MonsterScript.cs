using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


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

    private int layerMask_Player = 1 << 6;
    private int layerMask_SafeZone = 1 << 7;


    // Start is called before the first frame update
    void Start()
    {
 
        characterController = player.GetComponent<CharacterController>();
        agenteNav = gameObject.GetComponent<AgenteNav>();
    }


    // Update is called once per frame
    void Update()
    {
        //si el menu de pausa esta abierto, el monstruo no se mueve
        if (GameObject.FindWithTag("PauseMenu"))
        {
            agenteNav.pause();
            return;
        }
        else
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
            if (Physics.Raycast(playerRay, playerRangeDetection, layerMask_SafeZone) && oneTimeCheck == true && onGameReset == false)
            {
                Debug.Log("Player at SafeZone");
                agenteNav.patrol();
                oneTimeCheck = false;
            }
            if (!Physics.Raycast(playerRay, playerRangeDetection, layerMask_SafeZone) && oneTimeCheck == false && onGameReset == false)
            {
                agenteNav.chase();
                oneTimeCheck = true;
            }

            // CUANDO EL MONSTRUO TOCA EL JUGADOR: 
            if (Physics.Raycast(rayoDeteccion, monsterRangeHit, layerMask_Player) && onGameReset == false)
            {
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadScene(2);

                GameObject minijuegoActivo = GameObject.FindWithTag("Minijuego");
                if (minijuegoActivo)
                {
                    Destroy(minijuegoActivo);

                }
                // REINICIAR Y REALIZAR LA PAUSA
                StartCoroutine("gameReset");

                SceneManager.LoadScene(2); //CARGA EL MUNO DE CUANDO MUERES
            }
            agenteNav.resume();
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
