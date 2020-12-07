using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Door
{
    [SerializeField] private TimeManager time = null;
    [SerializeField] private GameObject portal = null;
    [SerializeField] private GameObject particles = null;

    // override the method to open the passage (Door)
    // activating the portal
    public new void OpenPassage()
    {
        if (time.IsPresent)
        {
            portal.gameObject.SetActive(true);
            particles.gameObject.SetActive(true);
        }
    }
}
