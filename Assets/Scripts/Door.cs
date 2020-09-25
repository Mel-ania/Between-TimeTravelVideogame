using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject
{
    // open the door
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
