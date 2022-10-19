using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EffectSystem;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "Item/Use Item")]
    public class UseItem : ItemBase
    {
        public EffectBase[] effectsOnUse;

        public void OnUseItem(Transform user)
        {
            for (int i = 0; i < effectsOnUse.Length; i++)
            {
                effectsOnUse[i].UseEffect(user);
            }
        }
    }
}