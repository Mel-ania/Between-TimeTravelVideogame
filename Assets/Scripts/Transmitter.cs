using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : InteractiveObject
{
    [SerializeField] private TimeManager isPresent = null;

    private Animator animator;

    private bool isActive = false;

    // property
    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isActive = !isPresent.IsPresent;
        if (isActive)
        {
            animator.SetBool("isActive", true);
        }
        else
        {
            animator.SetBool("isActive", false);
        }
    }
}
