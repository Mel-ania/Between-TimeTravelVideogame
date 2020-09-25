using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : InteractiveObject
{
    [SerializeField]
    private TimeManager isPresent = null;

    private bool isActive = false;

    // property
    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    private void Update()
    {
        isActive = !isPresent.IsPresent;
    }
}
