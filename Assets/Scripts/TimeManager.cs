using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemList = new List<Item>();

    private bool isPresent = true;

    // property
    public bool IsPresent
    {
        get
        {
            return isPresent;
        }
        set
        {
            isPresent = value;
        }
    }

    // change time from present to past or past to present (according to
    // the current time) and call the function that change the postiion
    // of the item in the scene
    public void ChangeTime()
    {
        isPresent = !isPresent;
        foreach (Item item in itemList)
        {
            item.ResetPosition();
        }
    }
}
