using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

[CreateAssetMenu(menuName = "Effect/Stat Modifier")]
public class StatModEffect : EffectBase
{
      // Variables to show in SO inspector
      public int healAmount;
      public int manaAmount;
      public int strengthBuffAmount;
      public int staminaBuffAmount;
      public int defenseBuffAmount;
      public int movementSpeedAmount;
      public StatModType statModType;


      // Apply the effect
      public override void OnTrigger(Character character)
      {
            if(numberOfEffects > 0)
            {
                  Debug.Log(effectDescription);
            }
            else
            {
                  Debug.Log($"No more uses left of {effectName}");
            }

      }



}
