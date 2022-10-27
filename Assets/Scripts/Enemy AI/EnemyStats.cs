using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour, IHittable
{
      [SerializeField] public int currentHealth;
      [SerializeField] public int maxHealth = 5;

      [SerializeField] bool isDead;

      ResourceDropper resourceDropper;
      NavMeshAgent agent;
      Animator anim;
      AI ai;

      private void Start()
      {
            currentHealth = maxHealth;
            anim = GetComponent<Animator>();
            ai = GetComponent<AI>();
            agent = GetComponent<NavMeshAgent>();
            resourceDropper = GetComponentInChildren<ResourceDropper>();
      }

      private void Update()
      {
            if (isDead)
            {
                  StartCoroutine(Die());

            }
      }

      // Can be used later to scale enemy damage, health, or any other value decided.
      public void GetHit(int damage)
      {
            if(isDead == false)
            {
                  currentHealth -= damage;

                  if(currentHealth <= 0)
                  {
                        isDead = true;
                  }
            }
      }

      IEnumerator Die()
      {
            anim.SetTrigger("isDead");
            agent.isStopped = true;
            yield return new WaitForSeconds(5f);
            resourceDropper.DropItem();
            LevelSystem.instance.AddExperience(30);
            Destroy(gameObject);
      }
}
