using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameManager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager pm;

        [SerializeField] private int _playerLevel = 1;
        [SerializeField] private int _currentEXP = 0;
        [SerializeField] private int _maxEXP = 100;

        [SerializeField] private float _curHealth;
        [SerializeField] private float _maxHealth = 100f;

        [SerializeField] private float _curStamina;
        [SerializeField] private float _maxStamina = 100f;

        [SerializeField] private float _curMana;
        [SerializeField] private float _maxMana = 100f;

        [SerializeField] private int _minAttack = 1;
        [SerializeField] private int _maxAttack = 2;

        [SerializeField] private int _healthPotionAmount;
        [SerializeField] private int _manaPotionAmount;

        [SerializeField] private float _healthPotionHeal = 40f;
        [SerializeField] private float _manaPotionHeal = 40f;

        [SerializeField] private float meatAmount = 0;

        public float newHealth = 0;
        public float newMana = 0;

        private float _MAXFILLAMOUNT = 1.0f;

        public bool healthRegen = false;
        public bool stamRegen = false;
        public bool manaRegen = false;

        public Image healthbar;
        public Image staminabar;
        public Image manabar;

        public float staminaFILLAMOUNT;
        public float healthFILLAMOUNT;
        public float manaFillAMOUNT;

        public int PlayerLevel { get { return _playerLevel; } set { _playerLevel = value; } }
        public int CurrentEXP { get { return _currentEXP; } set { _currentEXP = value; } }
        public int MaxEXP { get { return _maxEXP; } set { _maxEXP = value; } }

        public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
        public float CurrentHealth { get { return _curHealth; } set { _curHealth = value; } }

        public float MaxStamina { get { return _maxStamina; } set { _maxStamina = value; } }
        public float CurrentStamina { get { return _curStamina; } set { _curStamina = value; } }
      
        public float MaxMana { get { return _maxMana; } set { _maxMana = value; } }
        public float CurrentMana { get { return _curMana; } set { _curMana = value; } }

        public int MinAttack { get { return _minAttack; } set { _minAttack = value; } }
        public int MaxAttack { get { return _maxAttack; } set { _maxAttack = value; } }

        public float HealthPotionHeal { get { return _healthPotionHeal; } set { _healthPotionHeal = value; } }
        public float ManaPotionHeal { get { return _manaPotionHeal; } set { _manaPotionHeal = value; } }

        public int HealthPotionAmount { get { return _healthPotionAmount; } set { _healthPotionAmount = value; } }
        public int ManaPotionAmount { get { return _manaPotionAmount; } set { _manaPotionAmount = value; } }




        private void Awake()
         {
            if(PlayerManager.pm == null)
            {
                PlayerManager.pm = this;
            }
            else if(PlayerManager.pm != this)
            {
                Destroy(this);
            }
         }
        // Start is called before the first frame update
        void Start()
        {
            //Sets current health and stamina to max
            _curHealth = _maxHealth;
            _curStamina = _maxStamina;
            _curMana = _maxMana;

            //sets the healthbar and staminabar to max
            healthbar.fillAmount = _MAXFILLAMOUNT;
            staminabar.fillAmount = _MAXFILLAMOUNT;
            manabar.fillAmount = _MAXFILLAMOUNT;
        }

        // Update is called once per frame
        void Update()
        {
            //Displays current fill amount
            staminaFILLAMOUNT = DamageConversion(CurrentStamina, MaxStamina);
            healthFILLAMOUNT = DamageConversion(CurrentHealth, MaxHealth);
            manaFillAMOUNT = DamageConversion(CurrentMana, MaxMana);


            DisplayStatConversion(); //Converts health, stamina, and mana into fillamount for health, stamina, and mana bars
            StatsCheck(); // Checks to make sure numbers dont go below or above 0 and 100
        }
    

        //Converts regular values into values that can be used for the health and staminabar
        private float DamageConversion(float curStat, float maxStat)
        {
            var conversion = (curStat * _MAXFILLAMOUNT) / maxStat;

            return conversion;
        }

        private void StatsCheck()
        {
            //Makes sure health doesn't go over max and under 0
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            //Makes sure stamina doesn't go over max and under 0
            if (CurrentStamina > MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }
            if (CurrentStamina < 0)
            {
                CurrentStamina = 0;
            }

            //Makes sure mana doesn't go over max and under 0
            if (CurrentMana > MaxMana)
            {
                CurrentMana = MaxMana;
            }
            if (CurrentMana < 0)
            {
                CurrentMana = 0;
            }
        }

        private void DisplayStatConversion()
        {
            //converts amount and fluidly change health with lerp
            if (healthbar.fillAmount != DamageConversion(CurrentHealth, MaxHealth))
            {
                healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, DamageConversion(CurrentHealth, MaxHealth), 0.005f);
            }

            //converts amount and fluidly change stamina with lerp
            if (staminabar.fillAmount != DamageConversion(CurrentStamina, MaxStamina))
            {
                staminabar.fillAmount = Mathf.Lerp(staminabar.fillAmount, DamageConversion(CurrentStamina, MaxStamina), 0.005f);
            }

            //converts amount and fluidly change stamina with lerp
            if (manabar.fillAmount != DamageConversion(CurrentMana, MaxMana))
            {
                manabar.fillAmount = Mathf.Lerp(manabar.fillAmount, DamageConversion(CurrentMana, MaxMana), 0.005f);
            }
        }



            /// <summary>
            /// Used to detect pickups from mob drops, will be added to as we have more to pickup :D
            /// Items dropped MUST be on the Resource layer in order to be detected.
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                  if (other.gameObject.layer == LayerMask.NameToLayer("Resource"))
                  {
                        var resource = other.gameObject.GetComponent<Resources>();

                        if(resource != null)
                        {
                              switch (resource.ResourceData.ResourceEnum)
                              {
                                    case ResourceTypeEnum.MonsterMeat:
                                          meatAmount += resource.ResourceData.GetAmount();
                                          resource.PickupResource();
                                          break;
                              }
                        }
                        

                  }
            }

      }
}
