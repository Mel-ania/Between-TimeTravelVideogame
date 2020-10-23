using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Door
{
    [SerializeField]
    private TimeManager time = null;
    [SerializeField]
    private GameObject portal = null;
    [SerializeField]
    private GameObject particles = null;

    public override void OpenPassage()
    {
        if (time.IsPresent)
        {
            portal.gameObject.SetActive(true);
            particles.gameObject.SetActive(true);
        }
    }
}
