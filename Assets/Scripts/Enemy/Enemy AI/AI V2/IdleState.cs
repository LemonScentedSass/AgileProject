using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override StateType OnStateUpdate()
    {
        Debug.Log("Idle");

        if(_agent.target != null && Vector3.Distance(transform.position, _agent.target.position) > 10f)
        {
            return StateType.Chase;
        }

        return ReturnStateType();
    }

    public override StateType ReturnStateType()
    {
        return StateType.Idle;
    }
}
