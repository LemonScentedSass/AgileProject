using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
      [SerializeField] public CharacterStats currentTarget; // Assigned automatically by each state script, do not change from inspector for game use but would be good for testing.
      [SerializeField] public LayerMask detectionLayer; // Layer selection for player detection. Use layer "Player". 

      [Header("AI Settings")] // Edit all of these from inspector. 

      [Header("Idle Settings")]
      public float idleTimeBeforePatrol = 5f; // Currently does nothing. Used for PatrolState.

      [Header("Detection Settings")] // Higher and lower detection angles will give the agent a higher and lower field of view. 
      public float detectionRadius = 8f; // How far away the agent will begin to pursue the player.
      public float maximumDetectionAngle = 50f;
      public float minimumDetectionAngle = -50f;

      [Header("Pursuit Settings")]
      public float pursueSpeed = 5f; // How fast agent will chase the player.


      [Header("Attack Settings")]
      public float attackRange = 1f; // How far away from a target the agent must be to begin attacking.
      public float attackCooldownTimer = .88f; // Time between attacks.
      public bool isAttacking = false; // Used to check logic in inspector - do not change value in inspector for any reason besides testing.

      // Components that each script needs and can access.
      [HideInInspector] public Animator anim;
      [HideInInspector] public NavMeshAgent agent;

      private void Start()
      {
            // Both variables used to manipulate state scripts.
            anim = GetComponent<Animator>(); 
            agent = GetComponent<NavMeshAgent>();
      }


      
      private void OnDrawGizmos()
      {     

            // Show red wire sphere representing agent attackRange.
            if(attackRange > 0)
            {
                  Gizmos.color = Color.red;
                  Gizmos.DrawWireSphere(transform.position, attackRange);
            }

            // Show green wire sphere representing agent detectionRadius.
            if(detectionRadius > 0)
            {
                  Gizmos.color = Color.green;
                  Gizmos.DrawWireSphere(transform.position, detectionRadius);
            }
            
      }
}
