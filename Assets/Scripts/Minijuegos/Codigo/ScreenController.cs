using UnityEngine;
using TMPro;

public class ScreenController : MonoBehaviour
{
    public GameObject llamador;
    MinijuegoCodigo llamadorScript;

    public TextMeshProUGUI screenText;
    private string codigo = "";

    public void AddDigit(string digit)
    {   
        //Codigo limitado a 3 dígitos
        if (codigo.Length < 3) { 
            //Debug.Log("Numero: " + digit);
            codigo += digit;
            //Codigo en pantalla
            screenText.text = "Inserta clave: \n" + codigo; 
        }
    }

    public void CheckCode()
    {
        llamadorScript = llamador.GetComponent<MinijuegoCodigo>();
        //Debug.Log("Comprobando codigo: " + code);
        //Si el codigo es correcto, mensaje, si no mensaje incorrecto
        if (codigo == "123")
        {
            screenText.text = "Clave de acceso correcta";
            llamadorScript.EjecutarVictoria();
        }
        else
        {
            screenText.text = "Clave de acceso incorrecta";
        }
        //Vacia el codigo despues de la comprobacion
        codigo = "";
    }
}
