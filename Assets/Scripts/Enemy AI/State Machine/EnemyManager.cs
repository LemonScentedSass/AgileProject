using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
      [SerializeField] public PlayerManager currentTarget; // Assigned automatically by each state script, do not change from inspector for game use but would be good for testing.
      [SerializeField] public LayerMask detectionLayer; // Layer selection for player detection. Use layer "Player". 

      [Header("AI Settings")] // Edit all of these from inspector. 

      // Components that each script needs and can access.
      [HideInInspector] public Animator anim;
      [HideInInspector] public NavMeshAgent agent;

      private void Start()
      {
            // Both variables used to manipulate state scripts.
            anim = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
      }



      //private void OnDrawGizmos()
      //{     

      //      // Show red wire sphere representing agent attackRange.
      //      if(attackRange > 0)
      //      {
      //            Gizmos.color = Color.red;
      //            Gizmos.DrawWireSphere(transform.position, attackRange);
      //      }

      //      // Show green wire sphere representing agent detectionRadius.
      //      if(detectionRadius > 0)
      //      {
      //            Gizmos.color = Color.green;
      //            Gizmos.DrawWireSphere(transform.position, detectionRadius);
      //      }

      //}
}
