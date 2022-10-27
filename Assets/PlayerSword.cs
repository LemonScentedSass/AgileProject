using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
      [SerializeField] public int damageAmount = 1;                      // Weapon damage

      private void OnTriggerEnter(Collider other)
      {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // If we have collided with an enemy,
            {
                  var hittable = other.GetComponent<IHittable>();        // Grab the IHittable component
                  hittable.GetHit(damageAmount);                         // Call GetHit function passing damageAmount
                  Debug.Log("Hit enemy");
            }
      }
}