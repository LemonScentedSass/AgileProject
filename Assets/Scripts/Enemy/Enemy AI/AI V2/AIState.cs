using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected Agent _agent;

    public void Init(Agent agent)
    {
        _agent = agent;
    }

    public abstract StateType ReturnStateType();

    public virtual void OnStateEnter() { }
    public abstract StateType OnStateUpdate();
    public virtual void OnStateExit() { }

    public enum StateType
    { None, Idle, Chase, Wonder, Patrol }

}
