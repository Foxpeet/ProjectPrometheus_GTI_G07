using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public Camera cam;
    private Vector2 giro;
    private Vector3 movimientoRaw;
    private Vector3 movimientoFinal;
    private Vector3 camX;
    private Vector3 camZ;
    public float velocidad = 1f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        direccionCamara();

        movimientoRaw.x = Input.GetAxis("Horizontal");
        movimientoRaw.z = Input.GetAxis("Vertical");
        movimientoRaw = Vector3.ClampMagnitude(movimientoRaw, 1);

        giro.x += Input.GetAxis("Mouse X");
        giro.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-giro.y, giro.x, 0);

        movimientoFinal = movimientoRaw.x * camX + movimientoRaw.z * camZ;

        characterController.Move(movimientoFinal * velocidad * Time.deltaTime);
    }

    void direccionCamara()
    {
        camZ = cam.transform.forward;
        camX = cam.transform.right;

        camX.y = 0;
        camZ.y = 0;

        camX = camX.normalized;
        camZ = camZ.normalized;
    }
}
