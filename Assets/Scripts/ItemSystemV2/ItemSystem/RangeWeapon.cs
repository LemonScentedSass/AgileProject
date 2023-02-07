using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;

[CreateAssetMenu(menuName = "Weapon/Range")]
public class RangeWeapon : WeaponEquip, IProjectileObject
{
      public GameObject projectilePrefab;
      public float speed;
      public float distance;

      public int DamageRange { get { return CalculateDamage(); } }

      public override void AttackTrigger(Character owner, LayerMask combatMask)
      {
            Debug.Log("AttackTrigger Called - Instantiating Range Projectile");
            GameObject go = Instantiate(projectilePrefab, owner.transform.position + (owner.transform.forward * 2f + owner.transform.up * 2f), Quaternion.LookRotation(owner.transform.forward));

            if(go.GetComponent<Projectile>() == null)
            {
                  go.AddComponent<Projectile>();
            }

            go.GetComponent<Projectile>().Init(speed, owner, this, distance); 

      }
}
