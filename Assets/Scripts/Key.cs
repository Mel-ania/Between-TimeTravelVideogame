using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : CollectibleObject
{
    private KeyType keyType;
    [SerializeField] private KeyType startingKeyType = KeyType.Red;

    public enum KeyType
    {
        Red,
        Green,
        Blue
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
        keyType = startingKeyType;
    }
}
