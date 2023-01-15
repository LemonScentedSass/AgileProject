using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatsSave
{
    //STATS TO SAVE UPON ENTERING NEW SCENE
    public class StatsToken : MonoBehaviour
    {
        [System.Serializable]
        public class FakeManagerToken
        {
            public int SaveLevel;
            public int SaveHealth;
            public int SaveDamage;

            public FakeManagerToken(FakePlayerManager manager)
            {
                SaveLevel = manager.Level;
                SaveHealth = manager.Health;
                SaveDamage = manager.Damage;
            }
        }
    }
}

