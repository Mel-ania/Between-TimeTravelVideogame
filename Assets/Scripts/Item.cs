using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ObjectInScene
{
    public struct ItemState
    {
        public Vector2 Position;
        public Quaternion Rotation;

        public ItemState(Vector2 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    private ItemState currentState;
    private ItemState pastState;
    private ItemState presentState;

    [SerializeField]
    private TimeManager isPresent = null;

    // save the initial position in the item currentSTate
    private void Awake()
    {
        currentState = new ItemState(transform.position, transform.rotation);
    }

    private void Start()
    {
        pastState = currentState;
        isPresent.IsPresent = false;
    }

    // check if in the past, an item was move, and if so, reset the
    // item presentState
    private void Update()
    {
        if (!isPresent.IsPresent)
        {
            if (!pastState.Equals(currentState))
            {
                presentState = currentState;
            }
        }
    }

    // change the item position according to the current time
    public void ResetItemPosition()
    {
        if (isPresent.IsPresent)
        {
            currentState = presentState;
        }
        else
        {
            presentState = currentState;
            currentState = pastState;
        }
    }
}
