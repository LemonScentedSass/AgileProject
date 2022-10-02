using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
      public class PursueState : StateBase
      {
            public override StateType GetStateType { get { return StateType.Pursue; } }

            private EnemyManager enemyManager; // Reference to AI settings
            private Rigidbody playerRB; // Reference to target's Rigidbody

            public override void OnStateEnter()
            {
                  enemyManager = GetComponent<EnemyManager>(); // Assign a variable to enemy's settings.
                  enemyManager.agent.speed = enemyManager.pursueSpeed; // Adjust agent's speed for pursueState
                  playerRB = enemyManager.currentTarget.GetComponent<Rigidbody>(); // Assign a variable to player rigidbody.

            }

            public override StateType OnStateUpdate()
            {
                  float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position); // Calculate distance between player (target) and enemy.

                  
                  if (distanceFromTarget > enemyManager.attackRange) // If out of attack range, play pursuit anim and chase the player. 
                  {
                        enemyManager.anim.SetBool("isPursuing", true); // Start running animation
                        enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position); // Move agent toward target

                  }
                  if (distanceFromTarget <= enemyManager.attackRange && playerRB.velocity.magnitude <= 5.5f) // If agent is within the pursuit satisfaction range & the players magnitude is slowed, attack player.
                  {
                        return StateType.Attack;
                  }
                  else if(distanceFromTarget > enemyManager.attackRange && distanceFromTarget < enemyManager.detectionRadius) // Else if player (target) is outside of agent's FOV, return to idle. REPLACE WITH WANDER / WAYPOINT
                  {
                        return StateType.Pursue;
                  }
                  else
                  {
                        return StateType.Idle;
                  }

                  
            }

            public override void OnStateExit()
            {
                  enemyManager.agent.SetDestination(transform.position); // Stop agent by setting destination to own position.
                  enemyManager.anim.SetBool("isPursuing", false); // Stop animation.
            }
      }
}
