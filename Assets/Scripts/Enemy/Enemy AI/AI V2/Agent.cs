using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Transform target;
    private AIState _curState;

    private Dictionary<AIState.StateType, AIState> _states = new Dictionary<AIState.StateType, AIState>();

    private void Awake()
    {
        AIState[] states = GetComponents<AIState>();

        foreach (AIState state in states)
        {
            if(_states.ContainsKey(state.ReturnStateType()) == false)
            { 
                _states.Add(state.ReturnStateType(), state);
                state.Init(this);
            }
        }

        ChangeState(AIState.StateType.Idle);
    }

    private void Update()
    {
        if(_curState != null)
        {
            _curState.OnStateUpdate();
        }
    }

    private void ChangeState(AIState.StateType newState)
    {
        if(_curState == null || newState != _curState.ReturnStateType())
        {
            if(_states.ContainsKey(newState) != false)
            {
                if (_curState != null)
                {
                    _curState.OnStateExit();
                }

                _curState = _states[newState];
                _curState.OnStateEnter();
            }
        }
    }
}
