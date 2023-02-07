using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Timed Effect")]
public class TimedEffect : EffectBase
{
      public EffectBase[] onTick;

      public float duration;
      public int damagePerTick;
      [SerializeField] public ParticleSystem effectParticles;

      public override string GetEffectType { get { return "TimedEffect"; } }

      public override void OnTrigger(Character source, Character target)
      {
            ParticleSystem ps = Instantiate(effectParticles, target.transform.position, Quaternion.identity);
            target.gameObject.GetComponent<EnemyStats>().GetHit(damagePerTick);
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
