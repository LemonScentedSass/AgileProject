using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;

[CreateAssetMenu(menuName = "Weapon/Mage")]
public class MageWeapon : WeaponEquip, IProjectileObject
{
      public GameObject spellPrefab;
      public float speed;
      public float distance;

      public int DamageRange { get { return CalculateDamage(); } }

      public override void AttackTrigger(Character owner, LayerMask combatMask)
      {
            Debug.Log("Attack Trigger - Mage Weapon");

            GameObject go = Instantiate(spellPrefab, owner.transform.position + (owner.transform.forward * 5f + owner.transform.up * 2f), Quaternion.LookRotation(owner.transform.forward));

            if(go.GetComponent<Projectile>() == null)
            {
                  go.AddComponent<Projectile>();
            }

            go.GetComponent<Projectile>().Init(speed, owner, this, distance);

      }


}
