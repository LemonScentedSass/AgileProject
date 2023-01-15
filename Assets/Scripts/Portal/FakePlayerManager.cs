using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatsSave;



//Fake manager mimicking simple saves
    public class FakePlayerManager : MonoBehaviour
    {
        public static FakePlayerManager instance;

        public int Level = 1;
        public int Health = 10;
        public int Damage = 2;

        private void Awake()
        {
            if (FakePlayerManager.instance == null)
            {
                FakePlayerManager.instance = this;
            }
            else if (FakePlayerManager.instance != this)
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        //Load upon start
            Load();
        }


        //Saves the stats on the StatsToken script
        public void Save()
        {
            StatsSave.StatsSaveLoad.Save(instance);
        }
        
        //Loads save from StatsToken script if there is one
        public void Load()
        {
            StatsSave.StatsToken.FakeManagerToken data = StatsSaveLoad.Load();

            if (data != null)
            {
                Level = data.SaveLevel;
                Health = data.SaveHealth;
                Damage = data.SaveDamage;

            }
        }


    }


