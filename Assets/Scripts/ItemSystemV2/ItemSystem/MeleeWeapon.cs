using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemV2
{
    [CreateAssetMenu(menuName = "Weapon/Melee")]
    public class MeleeWeapon : WeaponEquip
    {
        public override void AttackTrigger(Character owner, LayerMask combatMask)
        {
            Collider[] targets = Physics.OverlapSphere(owner.transform.position + owner.transform.forward, 2f, combatMask);

            List<Character> list = new List<Character>();

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].GetComponent<Character>() != null)
                {
                    list.Add(targets[i].GetComponent<Character>());
                }
            }

            owner.DealDamage(list.ToArray());

            Debug.Log("Legnth: " + hitEffects.Length);

            for (int i = 0; i < hitEffects.Length; i++)
            {
                Debug.Log("Should sub");
                //Fix this later, not eveything is a timed effect
                TimedEffect te = hitEffects[i] as TimedEffect;

                for (int x = 0; x < list.Count; x++)
                {
                    list[x].SubscribeOnTick(te);
                }
            }
        }
    }
}

