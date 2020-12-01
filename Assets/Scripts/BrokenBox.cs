using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBox : StateMachineBehaviour
{
    // when the transition ends the box is not more active
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }
}
