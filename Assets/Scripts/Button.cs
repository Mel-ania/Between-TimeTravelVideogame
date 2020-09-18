using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private ButtonType buttonType;

    public enum ButtonType
    {
        Red,
        Green
    }

    public ButtonType GetButtonType()
    {
        return buttonType;
    }

    private void Start()
    {
        buttonType = ButtonType.Red;
    }

    public void ColorGreen()
    {
        buttonType = ButtonType.Green;
    }

}
