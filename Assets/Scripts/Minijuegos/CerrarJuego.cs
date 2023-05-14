using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !GameObject.FindWithTag("PauseMenu"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(this.gameObject, 1f);
        }
    }

    public void cerrarJuego()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(this.gameObject, 1f);
    }
}
