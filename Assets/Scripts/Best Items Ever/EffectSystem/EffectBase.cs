using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public abstract class EffectBase : ScriptableObject
{
      public string effectName;
      public string effectDescription;
      public int numberOfEffects;

      // When the effect is called, need to know who is using it and what target, if any.
      public abstract void OnTrigger(Character character);
}
