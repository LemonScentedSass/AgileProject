using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimListner : MonoBehaviour
{
      public LayerMask combatMask;

      [SerializeField] private Character owner;

      public void AttackAction()
      {
            owner.CallWeaponAttack();
      }



      //private void OnDrawGizmos()
      //{
      //      Gizmos.color = Color.red;
      //      Gizmos.DrawSphere(transform.position + transform.forward, 2f);
      //}
}
