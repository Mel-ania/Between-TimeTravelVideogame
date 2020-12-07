using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObject : InteractiveObject
{
    [SerializeField] private bool isTurning = true;
    [SerializeField] private GameObject on = null;
    [SerializeField] private GameObject off = null;

    private bool isActive;

    private void Awake()
    {
        isActive = on.activeSelf;
    }

    // when a dangerous oblect change state in different
    // periods of time, it has to turn on or off, so its active
    // state change
    public void TurnOnOff()
    {
        if (isTurning)
        {
            off.SetActive(isActive);
            isActive = !isActive;
            on.SetActive(isActive);
        }
    }
}
