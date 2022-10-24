using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace FSM
{
      public class IdleState : StateBase
      {
            public override StateType GetStateType { get { return StateType.Idle; } }

            private EnemyManager enemyManager; // Reference Settings script.

            [Header("Detection Settings")] // Higher and lower detection angles will give the agent a higher and lower field of view. 
            public float detectionRadius = 8f; // How far away the agent will begin to pursue the player.
            public float maximumDetectionAngle = 50f; // How far in the positive direction the enemy can see.
            public float minimumDetectionAngle = -50f; // How far in the negative direction the enemy can see. 

            [Header("Idle Settings")]
            public float idleTimeBeforePatrol = 5f; // Currently does nothing. Used for PatrolState.

            public override void OnStateEnter()
            {
                  enemyManager = GetComponent<EnemyManager>(); // Assign variable for settings script.
            }

            public override StateType OnStateUpdate()
            {
                  #region Player Detection
                  Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyManager.detectionLayer); // Physics sphere centered around agent's position with a radius of inspector set detection. Only detects collisions on the "Player" Layer.

                  for (int i = 0; i < colliders.Length; i++)
                  {
                        PlayerManager playerManager = colliders[i].transform.GetComponent<PlayerManager>(); // Loop through each collision *ALLOWS FOR FURTHER DEVELOPMENT* and save the PlayerStats component. This component stores health and stamina information.

                        if (playerManager != null)
                        {

                              Vector3 targetDirection = colliders[i].transform.position; // Set agent's target direction equal to the "player" being found through playerStats component.
                              float viewableAngle = Vector3.Angle(targetDirection, transform.forward); // Finds the angle between our target direction and the direction agent is facing.

                              if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                              {
                                    enemyManager.currentTarget = playerManager; // Assign the enemyManager scripts currentTarget variable equal to the playerStats component found in loop.  
                              }
                        }
                  }
                  #endregion

                  if (enemyManager.currentTarget != null) // As long as we have a target, pursue. Else return to idle. 
                  {
                        return StateType.Pursue;
                  }
                  else
                  {
                        return StateType.Idle;
                  }



            }

      }

}