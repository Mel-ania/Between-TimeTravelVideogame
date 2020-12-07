using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject
{
    [SerializeField] private Key.KeyType doorKeyType = Key.KeyType.Blue;

    // property
    public Key.KeyType IsDoorKeyType
    {
        get
        {
            return doorKeyType;
        }
    }

    // open the passage
    public void OpenPassage()
    {
        gameObject.SetActive(false);
    }
}
