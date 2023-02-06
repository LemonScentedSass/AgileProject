using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public abstract class EffectBase : ScriptableObject
{
    public string effectName;
    public string effectDescription;
    public int numberOfEffects;

    public abstract string GetEffectType { get; }

    // When the effect is called, need to know who is using it and what target, if any.
    public abstract void OnTrigger(Character source, Character target);

    public EffectToken GenerateToken(Character source, Character target)
    {
        EffectToken token = TokenUtilities.MakeEffectToken(source, target, this);
        return token;
    }
}
