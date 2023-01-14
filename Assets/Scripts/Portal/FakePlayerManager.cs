using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Load();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void Save()
    {
        SaveLoad.Save(instance);
    }
    public void Load()
    {
        FakeManagerToken data = SaveLoad.Load();

        if(data != null)
        {
            Level = data.SaveLevel;
            Health = data.SaveHealth;
            Damage = data.SaveDamage;

        }
    }


}
