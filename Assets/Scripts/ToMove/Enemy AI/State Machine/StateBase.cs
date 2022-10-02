using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
      public enum StateType { Idle, Patrol, Pursue, Attack }

      public abstract class StateBase : MonoBehaviour
      {
            protected FSMBase _agent;
            public abstract StateType GetStateType { get; }

            public virtual void OnStateEnter() { }
            public virtual void OnStateExit() { }
            public abstract StateType OnStateUpdate();
      }
}
