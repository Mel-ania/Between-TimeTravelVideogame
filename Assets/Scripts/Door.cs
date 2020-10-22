using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject
{
    // open the door
    public virtual void OpenPassage()
    {
        gameObject.SetActive(false);
    }
}
