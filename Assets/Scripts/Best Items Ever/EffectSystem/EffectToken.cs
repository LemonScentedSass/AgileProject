using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TokenUtilities
{
    public static EffectToken MakeEffectToken(Character source, Character target, EffectBase effect)
    {
        EffectToken newEffect = null;

        switch (effect.GetEffectType)
        {
            case "StatModEffect":

                break;
            case "TimedEffect":
                newEffect = new TimedEffectToken(source, target, effect);
                break;
        }


        return newEffect;
    }
}

public abstract class EffectToken
{
    protected Character _source;
    protected Character _target;

    public virtual void UpdateToken(float time)
    {

    }
}

public class TimedEffectToken : EffectToken
{
    private float elapsedTime;
    private float currentTickBetweenEvents = 0f;

    private TimedEffect _effect;

    public TimedEffectToken(Character source, Character target, EffectBase effect)
    {
        _effect = effect as TimedEffect;
        _source = source;
        _target = target;

        _effect.OnStart(_source, _target);
    }

    public override void UpdateToken(float time)
    {
        base.UpdateToken(time);
        elapsedTime += time;
        currentTickBetweenEvents += time;

        if (currentTickBetweenEvents >= _effect.tickRate * _effect.duration)
        {
            _effect.OnTick(_source, _target);
            currentTickBetweenEvents = 0f;
        }

        if(elapsedTime >= _effect.duration)
        {
            _effect.OnEnd(_source, _target);
            _source.UnsubscribeOnTick(this);
        }
    }
}
