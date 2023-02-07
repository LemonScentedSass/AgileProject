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

    public override string GetEffectType { get { return "StatModEffect"; } }

    // Apply the effect
    public override void OnTrigger(Character source, Character target)
    {
        target.AdjustHealth(healAmount);
    }
}
