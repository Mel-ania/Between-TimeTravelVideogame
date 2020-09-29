using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : CollectibleObject
{
    private KeyType keyType;

    public enum KeyType
    {
        Red,
        Green
    }

    // property
    public KeyType IsKeyType
    {
        get
        {
            return keyType;
        }
        set
        {
            keyType = value;
        }
    }

    // initialize the KeyType to Red
    private void Start()
    {
        keyType = KeyType.Red;
    }
}
