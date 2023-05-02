using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Jugar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(this.transform.parent.transform.parent.gameObject, 1f);
    }
    
    public void QuitarJuego()
    {
        SceneManager.LoadScene(0);
    }
}
