using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public State currentState;

    [SerializeField] public float attackRange = 2f;
    [SerializeField] private float turnSlerpSpeed = 5f;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        currentState = new Idle(this.gameObject, agent, anim, player);
    }

    private void Update()
    {
        currentState = currentState.Process();

        if (agent == null)                              // ??????????
        {
            currentState.Exit();
        }
    }
    
    private void OnAnimatorMove()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.velocity = anim.deltaPosition / Time.deltaTime;

        Quaternion newRot = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, turnSlerpSpeed * Time.deltaTime);
    }


}
