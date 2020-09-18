using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPosition : MonoBehaviour
{
    [SerializeField]
    private Item currentItem;
    private Item pastItem;
    private Item presentItem;

    private bool isPresent = true;

    public bool Present
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


    private void Start()
    {
        pastItem = currentItem.Copy();
        this.Present = false;
    }

    private void Update()
    {
        if(!isPresent)
        {
            if (!pastItem.Equals(currentItem))
            {
                presentItem = currentItem.Copy();
            }
        }
    }

    public void TimeChanger(bool timePresent)
    {
        if (timePresent)
        {
            isPresent = true;
            currentItem = presentItem.Copy();
        }
        else
        {
            isPresent = false;
            presentItem = currentItem.Copy();
            currentItem = pastItem.Copy();
        }
    }
}
