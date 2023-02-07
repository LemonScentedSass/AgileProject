using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace ItemSystemV2
{
      public abstract class WeaponEquip : EquipItem
      {
            // Settings for weapon SO

            public Vector2Int minimumDamageRange;
            public Vector2Int maximumDamageRange;
            public AttackSpeed attackSpeed;
            public DamageTypes damageTypes;

            // Array for when we hit a target, play these effects
            public EffectBase[] hitEffects;

            public GameObject weaponModel;

            public void DealDamage(IHittable target)
            {
                  target.AdjustHealth(CalculateDamage());
            }

            public virtual int CalculateDamage()
            {
                  int min = Random.Range(minimumDamageRange.x, minimumDamageRange.y);
                  int max = Random.Range(maximumDamageRange.x, maximumDamageRange.y);

                  int damage = Random.Range(min, max);

                  return damage;
            }

            public override void OnEquip(Character character)
            {
                  base.OnEquip(character);

                  for (int i = 0; i < hitEffects.Length; i++)
                  {
                        character.onHit += hitEffects[i].OnTrigger;
                  }
            }

            public override void OnUnequip(Character character)
            {
                  base.OnUnequip(character);

                  GameObject.Destroy(character.RightHandWeaponMount.GetChild(0));

                  for (int i = 0; i < hitEffects.Length; i++)
                  {
                        character.onHit -= hitEffects[i].OnTrigger;
                  }
            }

            // Melee Weapon Attack
            public abstract void AttackTrigger(Character owner, LayerMask combatMask);

            public enum AttackSpeed
            {
                  VerySlow,
                  Slow,
                  Normal,
                  Fast,
                  VeryFast
            }


      }

}

