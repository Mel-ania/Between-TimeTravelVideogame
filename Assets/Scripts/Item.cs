using System;
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

        // change the position and the rotation of a state
        public void ChangeState(Vector2 newPosition, Quaternion newRotation)
        {
            Position = newPosition;
            Rotation = newRotation;
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
        pastState = currentState;
        presentState = currentState;
    }

    // check if in the past, an item was move, and if so, reset the
    // item presentState
    private void Update()
    {
        currentState.ChangeState(transform.position, transform.rotation);
        if (!isPresent.IsPresent)
        {
            if(StateDifference(pastState, currentState) > 0.1)
            {
                presentState = currentState;
            }
        }
    }

    // change the item position according to the current time
    public void ResetPosition()
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
        transform.position = currentState.Position;
        transform.rotation = currentState.Rotation;
    }

    // calculate the difference between two states
    private float StateDifference(ItemState is1, ItemState is2)
    {
        float xPosition, yPosition, xRotation, yRotation;
        float diffPosition, diffRotation;

        xPosition = Math.Abs(is1.Position.x - is2.Position.x);
        yPosition = Math.Abs(is1.Position.y - is2.Position.y);
        xRotation = Math.Abs(is1.Rotation.x - is2.Rotation.x);
        yRotation = Math.Abs(is1.Rotation.y - is2.Rotation.y);

        diffPosition = xPosition + yPosition;
        diffRotation = xRotation + yRotation;
        
        return (diffPosition > diffRotation)? diffPosition : diffRotation;
    }
}
