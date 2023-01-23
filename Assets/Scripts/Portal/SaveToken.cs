using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerSaving
{
    //STATS TO SAVE UPON ENTERING NEW SCENE
    public class SaveToken : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerSaveToken
        {
            public int Level;
            public int EXP;

            public int Health;
            public int Stamina;
            public int Mana;

            public int Damage;

            public PlayerSaveToken(GameManager.PlayerManager manager)
            {
                Level = manager.GetComponent<LevelSystem>().level;
            }
        }
    }
}

