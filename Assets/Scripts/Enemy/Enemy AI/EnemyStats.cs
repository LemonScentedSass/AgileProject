using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameManager;

public class EnemyStats : Character, IHittable
{
      [SerializeField] public int currentHealth;
      [SerializeField] public int maxHealth = 5;

      [SerializeField] public bool isDead;

      ResourceDropper resourceDropper;
      NavMeshAgent agent;

      Animator anim;
      AI ai;

      private new void Start()
      {
            currentHealth = maxHealth;
            anim = GetComponent<Animator>();
            ai = GetComponent<AI>();
            agent = GetComponent<NavMeshAgent>();
            resourceDropper = GetComponentInChildren<ResourceDropper>();
      }

      private void Update()
      {
            
      }

      // Can be used later to scale enemy damage, health, or any other value decided.
      public void GetHit(int damage)
      {
            Debug.Log("GetHit - EnemyStats");
            if (isDead == false)
            {
                  //switch (type)
                  //{
                  //      case DamageTypes.Fire:
                  //            gameObject.GetComponentInChildren<ParticleSystem>().Play();
                  //            break;
                  //}

                  GetComponent<EnemyHealthUI>().healthSlider.enabled = true;

                  currentHealth -= damage;

                  if (currentHealth <= 0)
                  {
                        GetComponent<EnemyHealthUI>().healthSlider.enabled = false;
                        isDead = true;
                        Die();
                  }
            }
      }

      void Die()
      {
            anim.SetBool("isDead", true);
            agent.isStopped = true;
            resourceDropper.DropItem();
            LevelSystem.instance.AddExperience(30);
            Destroy(this.gameObject, 3f);
      }

      public void GetStunned(float length)
      {
            if (length > 0)
            {
                  agent.speed = 0f;
                  anim.ResetTrigger("isWalking");
                  anim.ResetTrigger("isRunning");
                  anim.ResetTrigger("isAttacking");
                  anim.ResetTrigger("isIdle");
                  length -= Time.deltaTime;
            }

            if (length <= 0)
            {
                  agent.speed = 2f;
            }

      }
}
