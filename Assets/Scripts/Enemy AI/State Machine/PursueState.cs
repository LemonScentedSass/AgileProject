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
            private AttackState attackState;

            [Header("Pursuit Settings")]
            public float pursueSpeed = 5f; // How fast agent will chase the player.
            public float maxPursuitDistance = 10f; // How far player can be before AI stops chasing.


            public override void OnStateEnter()
            {
                  enemyManager = GetComponent<EnemyManager>(); // Assign a variable to enemy's settings.
                  enemyManager.agent.speed = pursueSpeed; // Adjust agent's speed for pursueState
                  attackState = GetComponent<AttackState>(); // Reference attackState to pull attackRange variable for switching states. 

            }

            public override StateType OnStateUpdate()
            {
                  float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position); // Calculate distance between player (target) and enemy.

                  if (distanceFromTarget > maxPursuitDistance) // If player is further than our maxPursuitDistance, return to Idle.
                  {
                        return StateType.Idle;
                  }

                  if (distanceFromTarget < attackState.attackRange) // If player is close enough to attack, move to attack state.
                  {
                        return StateType.Attack;
                  }
                  else // Else just continue to pursue. 
                  {
                        enemyManager.anim.SetBool("isPursuing", true);
                        enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);
                        return StateType.Pursue;
                  }



                  //if (distanceFromTarget > enemyManager.attackRange) // If out of attack range, play pursuit anim and chase the player. 
                  //{
                  //      enemyManager.anim.SetBool("isPursuing", true); // Start running animation
                  //      enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position); // Move agent toward target

                  //}
                  //if (distanceFromTarget <= enemyManager.attackRange && playerRB.velocity.magnitude <= 5.5f) // If agent is within the pursuit satisfaction range & the players magnitude is slowed, attack player.
                  //{
                  //      return StateType.Attack;
                  //}
                  //else if(distanceFromTarget > enemyManager.attackRange && distanceFromTarget < maxPursuitDistance) // Else if player (target) is outside of agent's FOV, return to idle. REPLACE WITH WANDER / WAYPOINT
                  //{
                  //      return StateType.Pursue;
                  //}
                  //else
                  //{
                  //      return StateType.Idle;
                  //}


            }

            public override void OnStateExit()
            {
                  enemyManager.agent.SetDestination(transform.position); // Stop agent by setting destination to own position.
                  enemyManager.anim.SetBool("isPursuing", false); // Stop animation.
            }
      }
}
