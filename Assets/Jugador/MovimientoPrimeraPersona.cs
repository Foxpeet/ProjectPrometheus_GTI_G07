using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrimeraPersona : MonoBehaviour {

    // Objeto CharacterController que controla el movimiento del jugador
    public CharacterController controller;

    // Velocidad, por defecto, de movimiento normal
    public float speed = 12f;

    // Velocidad, por defecto, de movimiento al correr
    public float runSpeed = 24f;

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

    void Start() {
        // Establecer la cantidad inicial de stamina a la cantidad máxima
        currentStamina = maxStamina;
    }

    void Update() {
        // Obtener la entrada de movimiento horizontal y vertical
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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
                    speed = 12f;
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
                    speed = runSpeed;
                }
            }

            // Mover al jugador basado en la dirección y velocidad de movimiento
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
