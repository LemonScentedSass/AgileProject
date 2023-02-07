using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Timed Effect")]
public class TimedEffect : EffectBase
{
    public EffectBase[] onTick;

    public float duration;
    public int damagePerTick;
    [Range(0.1f ,1f)] public float tickRate;

    public override string GetEffectType { get { return "TimedEffect"; } }

    public override void OnTrigger(Character source, Character target)
    {
        target.gameObject.GetComponent<EnemyStats>().AdjustHealth(damagePerTick);
    }

    public void OnTick(Character source, Character target)
    {
        for (int i = 0; i < onTick.Length; i++)
        {
            onTick[i].OnTrigger(source, target);
        }
    }
}
