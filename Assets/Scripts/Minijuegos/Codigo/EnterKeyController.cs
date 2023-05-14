using UnityEngine;

public class EnterKeyController : MonoBehaviour
{
    public ScreenController screenController;

    private void OnMouseDown()
    {
        
        screenController.CheckCode();
    }
}
