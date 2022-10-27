using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
      [SerializeField] int damageAmount = 1;

      private void OnTriggerEnter(Collider other)
      {
            if(other.gameObject.tag == "Player")
            {
                  other.GetComponent<IHittable>().GetHit(damageAmount);
                  Debug.Log("Hit Player");
            }
      }
}
