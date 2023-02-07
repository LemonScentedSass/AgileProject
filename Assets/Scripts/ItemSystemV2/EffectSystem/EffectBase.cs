using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public abstract class EffectBase : ScriptableObject
{
    public string effectName;
    public string effectDescription;

    public EffectBase[] onStart;
    public EffectBase[] onEnd;

    public abstract string GetEffectType { get; }

    // When the effect is called, need to know who is using it and what target, if any.
    public abstract void OnTrigger(Character source, Character target);

    public virtual void OnStart(Character source, Character target)
    {
        Debug.Log("OnStart From EffectBase");
        for (int i = 0; i < onStart.Length; i++)
        {
            onStart[i].OnStart(source, target);       //Changed from onStart[i].OnTrigger(source, target);
        }
    }

    public virtual void OnEnd(Character source, Character target)
    {
        Debug.Log("Attempting to end VFX");
        for (int i = 0; i < onEnd.Length; i++)
        {
            onEnd[i].OnEnd(source, target);         //Changed from onEnd[i].OnTrigger(source, target);
        }
    }

    public EffectToken GenerateToken(Character source, Character target)
    {
        EffectToken token = TokenUtilities.MakeEffectToken(source, target, this);
        return token;
    }
}
