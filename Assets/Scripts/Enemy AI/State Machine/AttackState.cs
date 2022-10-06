using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{

      public class AttackState : StateBase
      {
            public override StateType GetStateType { get { return StateType.Attack; } }

            private EnemyManager enemyManager; // Enemy Manager is a script located on the enemy game object that controls all settings for each state. Change values through the inspector.
            private float timerForNextAttack; // Timer variable

            [Header("Attack Settings")]
            [SerializeField] public float attackRange = 1f; // How far away from a target the agent must be to begin attacking.
            [SerializeField] public float attackCooldownTimer = .88f; // Time between attacks.
            [SerializeField] public bool isAttacking = false; // Used to check logic in inspector - do not change value in inspector for any reason besides testing.
            [SerializeField] public float bufferDistance = 2f; // Buffer distance helps prevent jittering upon attack threshold.

            public override void OnStateEnter()
            {
                  enemyManager = GetComponent<EnemyManager>(); // Assign a reference variable to our settings.
                  timerForNextAttack = 0; // Start the state with timer set to 0.
            }

            public override StateType OnStateUpdate()
            {
                  float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position); // Calculate distance between the player and enemy transforms.

                  if (timerForNextAttack > 0)
                  {
                        timerForNextAttack -= Time.deltaTime; // Update timer by -time.deltaTime
                  }
                  else if (timerForNextAttack <= 0 && enemyManager.currentTarget.CurrentHealth > 0) // if the timer has run out AND the player (target) is still alive, preform another attack.
                  {
                        // *UPDATE TO ROOT MOVEMENT*

                        enemyManager.agent.SetDestination(transform.position); // Stop agent by setting desination to agent's transform.
                        enemyManager.anim.transform.LookAt(enemyManager.currentTarget.transform); // Rotate agent to look at player (target).
                        enemyManager.anim.SetBool("isAttacking", true); // Set isAttacking bool to true, plays animation.
                        timerForNextAttack = attackCooldownTimer; // Reset timer to the inspector set cooldown timer. 
                  }

                  if (distanceFromTarget > attackRange + bufferDistance)
                  {
                        return StateType.Pursue;
                  }
                  else if (enemyManager.currentTarget.CurrentHealth <= 0)
                  {
                        return StateType.Idle;
                  }
                  else
                  {
                        return StateType.Attack;
                  }






                  //if (enemyManager.currentTarget.currentHealth <= 0) //If Current target is dead, return to idle.
                  //{
                  //      return StateType.Idle;
                  //}
                  ////pursuit reactivation range
                  //else if (distanceFromTarget > attackRange) // If target is outside of attackRange, return to pursue.
                  //{
                  //      return StateType.Pursue;
                  //}
                  //else
                  //{
                  //      return StateType.Attack;
                  //}





            }

            public override void OnStateExit()
            {
                  enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position); // Set agent's destination to player (target) transform.
                  enemyManager.anim.SetBool("isAttacking", false); // Stop Animation.
            }
      }
}
