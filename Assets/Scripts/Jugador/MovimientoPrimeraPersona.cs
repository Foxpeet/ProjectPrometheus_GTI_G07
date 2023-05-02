using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrimeraPersona : MonoBehaviour {

    // Objeto CharacterController que controla el movimiento del jugador
    public CharacterController controller;

    // Velocidad, por defecto, de movimiento normal
    public float speed;

    // Velocidad, por defecto, de movimiento al correr
    public float runSpeed;

    // Velocidad final definida por el estado en el que se encuentre
    public float finalSpeed;

    // Cantidad máxima de stamina que el jugador puede tener
    public float maxStamina = 10f;

    // Tasa, por defecto, de regeneración de la stamina
    public float staminaRegenRate = 1f;

    // Tecla para correr -> se puede modificar más adelante en la interfaz también
    public KeyCode runKey = KeyCode.LeftShift;

    // Booleano que indica si el jugador está corriendo
    private bool isRunning = false;

    // Cantidad actual de stamina del jugador
    private float currentStamina;

    //el objeto de menu de pausa
    public GameObject PauseMenu;

    void Start() {
        // Establecer la cantidad inicial de stamina a la cantidad máxima
        currentStamina = maxStamina;

        // Establecer en un primer estado la velocidad final como la velocidad normal
        finalSpeed = speed;
    }

    void Update() {
        // Obtener la entrada de movimiento horizontal y vertical
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.P) && !GameObject.FindWithTag("PauseMenu"))
        {
            Instantiate(PauseMenu);
            return;
        }

        if (!GameObject.FindWithTag("PauseMenu"))
        {
            if (!GameObject.FindWithTag("Minijuego"))
            {
                // Calcular la dirección del movimiento basada en la entrada
                Vector3 move = transform.right * x + transform.forward * z;

                // Si el jugador está corriendo
                if (isRunning)
                {
                    // Reducir la stamina mientras se corre
                    currentStamina -= Time.deltaTime;
                    if (currentStamina <= 0)
                    {
                        // Si la stamina llega a cero, dejar de correr y volver a la velocidad normal
                        currentStamina = 0;
                        isRunning = false;
                        finalSpeed = speed;
                    }
                }
                else
                {
                    // Regenerar la stamina mientras no se corre
                    if (currentStamina < maxStamina)
                    {
                        currentStamina += Time.deltaTime * staminaRegenRate;
                    }
                    // Si se presiona la tecla de correr y hay suficiente stamina
                    // La stamina mínima para empezar a correr es 0.1f unidades de tiempo
                    if (Input.GetKeyDown(runKey) && currentStamina >= 0.1f)
                    {
                        // Empezar a correr y aumentar la velocidad
                        isRunning = true;
                        finalSpeed = runSpeed;
                    }
                }

                // Mover al jugador basado en la dirección y velocidad de movimiento
                controller.Move(move * finalSpeed * Time.deltaTime);
            }
        }
    }
}
