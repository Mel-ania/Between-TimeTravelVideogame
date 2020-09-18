using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public Vector2 position;
	public Quaternion rotation;

	public Item(Vector2 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}

	public Item Copy()
    {
		return new Item(this.position, this.rotation);
    }

	public void ChangePosition(Vector2 newPosition, Quaternion newRotation)
    {
		position = newPosition;
		rotation = newRotation;
    }
}
