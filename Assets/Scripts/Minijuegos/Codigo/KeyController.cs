using UnityEngine;

public class KeyController : MonoBehaviour
{
    public string digit;
    public ScreenController screenController;

    private void OnMouseDown()
    {
        screenController.AddDigit(digit);
    }
}
