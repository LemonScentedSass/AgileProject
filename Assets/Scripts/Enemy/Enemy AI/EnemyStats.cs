using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameManager;

public class EnemyStats : Character, IHittable
{

      ResourceDropper resourceDropper;
      NavMeshAgent agent;

      Animator anim;
      AI ai;

      private new void Start()
      {
            _curHealth = _maxHealth;
            anim = GetComponent<Animator>();
            ai = GetComponent<AI>();
            agent = GetComponent<NavMeshAgent>();
            resourceDropper = GetComponentInChildren<ResourceDropper>();
      }

      protected override void Update()
      {
            base.Update();
      }

      // Can be used later to scale enemy damage, health, or any other value decided.
      public void AdjustHealth(int damage)
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

                  _curHealth -= damage;

                  if (_curHealth <= 0)
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
