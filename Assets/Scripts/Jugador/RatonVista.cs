using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatonVista : MonoBehaviour
{

    public float mouseSensitivity = 100f; // La sensibilidad que va a tener el ratón por defecto
    public Transform playerBody; // El cuerpo del jugador
    public float xRotation = 0f; // La rotación en el eje X

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("PauseMenu"))
        {
            if (!GameObject.FindWithTag("Minijuego"))
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // El movimiento del ratón en el eje X
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // El movimiento del ratón en el eje Y

                xRotation -= mouseY; // La rotación en el eje X se actualiza con el movimiento del ratón en el eje Y
                xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Se limita la rotación en el eje X a un máximo de 90 grados hacia arriba y hacia abajo

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Se actualiza la rotación del objeto que tiene este script (la cámara) en función de la rotación en el eje X
                playerBody.Rotate(Vector3.up * mouseX); // Se rota el cuerpo del jugador en función del movimiento del ratón en el eje X
            }
        }
        
    }
}
