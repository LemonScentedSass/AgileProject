using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHittable
{
      public NavMeshAgent agent;
      public Animator anim;

      public Transform player;

      public LayerMask whatIsGround, whatIsPlayer;

      public float health = 3;

      //Patroling
      public Vector3 walkPoint;
      bool walkPointSet;
      public float walkPointRange;

      //Attacking
      public float timeBetweenAttacks;
      bool alreadyAttacked;
      public GameObject projectile;

      // Chase
      [SerializeField] private float speed;
      Vector3 velocity = Vector3.zero;

      //States
      public float sightRange, attackRange;
      public bool playerInSightRange, playerInAttackRange;

      private void Awake()
      {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            agent.updatePosition = false;
      }

      private void Update()
      {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) StartCoroutine(Attack());

            if (health <= 0) Death();
      }

      private void Death()
      {
            var itemDropper = GetComponentInChildren<ResourceDropper>();
            itemDropper.DropItem();
            Destroy(gameObject);
      }

      private void Patroling()
      {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                  agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                  walkPointSet = false;
      }
      private void SearchWalkPoint()
      {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                  walkPointSet = true;
      }

      private void ChasePlayer()
      {
            if (agent.speed == 0)
                  agent.speed = speed;

            anim.SetBool("isAttacking", false);
            anim.SetBool("isChasing", true);
            agent.SetDestination(player.position);

            transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f);
      }

      private void AttackPlayer()
      {
            
      }
      private void ResetAttack()
      {
            alreadyAttacked = false;
      }

      private void OnDrawGizmosSelected()
      {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
      }

      public IEnumerator Attack()
      {
            //Make sure enemy doesn't move
            anim.SetBool("isChasing", false);
            anim.SetBool("isAttacking", true);
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                  Debug.Log("Attacking");

                  alreadyAttacked = true;
                  Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
      }

      public void GetHit(int damage)
      {
            if (health > 0)
            {
                  health -= damage;
                  if(health <= 0)
                  {
                        Death();
                  }
            }
      }
}
