using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
      public class FSMBase : MonoBehaviour
      {
            [SerializeField] protected StateBase _curState;
            [SerializeField] protected StateType _startingState;

            protected Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();

            // Start is called before the first frame update
            protected void Start()
            {
                  StateBase[] states = GetComponents<StateBase>();

                  for (int i = 0; i < states.Length; i++)
                  {
                        if (_states.ContainsKey(states[i].GetStateType) == false)
                        {
                              _states.Add(states[i].GetStateType, states[i]);
                        }
                  }

                  GoToState(_startingState);
            }

            // Update is called once per frame
            protected void Update()
            {
                  if (_curState != null)
                  {
                        GoToState(_curState.OnStateUpdate());
                  }
            }

            protected void GoToState(StateType newState)
            {
                  if (_curState == null)
                  {
                        if (_states.ContainsKey(newState) == false)
                        {
                              if (_states.ContainsKey(StateType.Idle) == true)
                              {
                                    _curState = _states[StateType.Idle];
                                    _curState.OnStateEnter();
                              }
                              else
                              {
                                    Debug.LogError(name + " has no valid " + newState.ToString() + " or idle states");
                              }
                        }
                        else
                        {
                              _curState = _states[newState];
                              _curState.OnStateEnter();
                        }
                  }
                  else
                  {
                        if (_states.ContainsKey(newState) == false)
                        {
                              return;
                        }

                        if (newState != _curState.GetStateType)
                        {
                              _curState.OnStateExit();
                              _curState = _states[newState];
                              _curState.OnStateEnter();
                        }
                  }
            }
      }
}



