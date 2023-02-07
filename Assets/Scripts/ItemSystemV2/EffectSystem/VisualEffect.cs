using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Visual Effects")]
public class VisualEffect : TimedEffect
{
      public GameObject visualPrefab;

      public override string GetEffectType { get { return "VisualEffect"; } }

      public override void OnTrigger(Character source, Character target)
      {

      }

      public override void OnStart(Character source, Character target)
      {
            Debug.Log("Instantiate");
            Transform t = GameObject.Instantiate(visualPrefab, target.transform.position + Vector3.up * 2f, Quaternion.identity, target.transform).transform;
            target.RegisterVisualEffect(this, t);
      }

      public override void OnEnd(Character source, Character target)
      {
            target.UnregisterVisualEffect(this);
      }
}