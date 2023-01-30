using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomValueSetter : StateMachineBehaviour
{
    public int randomMax;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rng = Random.Range(0, randomMax + 1);
        animator.SetFloat("attackIndex", rng);
    }
}
