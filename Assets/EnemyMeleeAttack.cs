using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
      [SerializeField] public int damageAmount = 1;                                 // Weapon damage

      private void OnTriggerEnter(Collider other)
      {


            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))          // If we have collided with an enemy,
            {
                  Debug.Log("Collided with player");

                  var hittable = other.GetComponent<GameManager.PlayerManager>();                   // Grab the IHittable component
                  if (hittable != null)
                  {
                        hittable.GetHit(damageAmount);                              // Call GetHit function passing damageAmount
                        Debug.Log("Hit Player");
                  }
                  
            }
      }


}
