using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    [SerializeField]
    private ButtonHolder buttonHolder;

    private bool isActive = false;

    private void Update()
    {
        if (buttonHolder.TimePresent())
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }

    public bool Active()
    {
        return isActive;
    }
}
