using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Timed Effect")]
public class TimedEffect : EffectBase
{
    public EffectBase[] onTick;

    public int duration;

    public override string GetEffectType { get { return "TimedEffect"; } }

    public override void OnTrigger(Character source, Character target)
    {
        throw new System.NotImplementedException();
    }

    public void OnTick(Character source, Character target)
    {
        Debug.Log("Tick Yo!");

        for (int i = 0; i < onTick.Length; i++)
        {
            onTick[i].OnTrigger(source, target);
        }
    }
}
