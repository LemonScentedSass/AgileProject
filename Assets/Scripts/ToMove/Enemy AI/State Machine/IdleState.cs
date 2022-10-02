using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
      public class IdleState : StateBase
      {
            public override StateType GetStateType { get { return StateType.Idle; } }

            private EnemyManager enemyManager; // Reference Settings script.

            public override void OnStateEnter()
            {
                  enemyManager = GetComponent<EnemyManager>(); // Assign variable for settings script.
            }

            public override StateType OnStateUpdate()
            {
                  #region Player Detection
                  Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, enemyManager.detectionLayer); // Physics sphere centered around agent's position with a radius of inspector set detection. Only detects collisions on the "Player" Layer.

                  for (int i = 0; i < colliders.Length; i++)
                  {
                        PlayerStats playerStats = colliders[i].transform.GetComponent<PlayerStats>(); // Loop through each collision *ALLOWS FOR FURTHER DEVELOPMENT* and save the PlayerStats component. This component stores health and stamina information.

                        if (playerStats != null) 
                        {
                              Vector3 targetDirection = colliders[i].transform.position; // Set agent's target direction equal to the "player" being found through playerStats component.
                              float viewableAngle = Vector3.Angle(targetDirection, transform.forward); // Finds the angle between our target direction and the direction agent is facing.

                              if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                              {
                                    enemyManager.currentTarget = playerStats; // Assign the enemyManager scripts currentTarget variable equal to the playerStats component found in loop.  
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