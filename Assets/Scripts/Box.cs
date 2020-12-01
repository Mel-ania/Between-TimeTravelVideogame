using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{
    //private Animator animator;
    private BoxCollider2D boxC;

    private bool isLanded = true;
    private bool isGrounded = false;
    private float yPosition;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        boxC = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    { 
        // check if the box is touching the ground
        isGrounded = boxC.IsTouchingLayers();
    }

    private void Update()
    {
        // check if the height from the box falls is too much,
        // if it is, the box it's broken
        if (!isGrounded && isLanded)
        {
            isLanded = false;
            yPosition = transform.position.y;
        }
        else if (isGrounded && !isLanded)
        {
            isLanded = true;
            if (yPosition - transform.position.y > 5) {
                //animator.SetTrigger("broken");
                gameObject.SetActive(false);
            }

        }
    }
}
