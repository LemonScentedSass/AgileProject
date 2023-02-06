using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace ItemSystemV2
{
      public abstract class EquipItem : ItemBase
      {
            public EffectBase[] passiveEffects;
            // Make array for on equip effects, passive effects include +2 health, + 5 stamina, etc

            // List of what we want to happen when equipping an item (weapon or armor)
            // Need to have the effects applied to the character who is using the item
            public virtual void OnEquip(Character character)
            {

            }

            // List of what we want to happen when unequipping an item
            // Need to remove the effects applied to the character who is using the item
            public virtual void OnUnequip(Character character)
            {

            }
      }
}

