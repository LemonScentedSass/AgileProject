using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace GameManager
{
    public class PlayerManager : MonoBehaviour, IHittable
    {
        public static PlayerManager pm;
        Animator anim;
        PlayerLocomotion playerLoco;
        private AudioManager _am;

        [Header("Player Health / Stamina / Mana")]
        [SerializeField] public float _curHealth;
        [SerializeField] public float _maxHealth = 10f;
        [SerializeField] public float _curStamina;
        [SerializeField] public float _maxStamina = 100f;
        [SerializeField] public float _staminaRegenTime = 4f;
        [SerializeField] public float _curMana;
        [SerializeField] public float _maxMana = 100f;

        [SerializeField] public int skillPoints = 0;


        [Header("Attack Settings?")]
        [SerializeField] public int _minAttack = 1;
        [SerializeField] public int _maxAttack = 2;

        [Header("Consumable Settings")]
        [SerializeField] private int _healthPotionAmount;
        [SerializeField] private int _manaPotionAmount;
        [SerializeField] private int _buffPotionAmount;

        [SerializeField] private float _healthPotionHeal = 40f;
        [SerializeField] private float _manaPotionHeal = 40f;

        [Header("Collectables")]
        [SerializeField] public float meatAmount = 0;
        [SerializeField] public int goldAmount = 0;


        [Header("Others")]
        private float _MAXFILLAMOUNT = 1.0f;
        [SerializeField] private TMPro.TMP_Text GoldAmountTXT;

        [Header("Audio")]
        public AudioClip goldPickup, expPickup, bottlePickup;

        private bool isDead = false;
        public bool usingItem = false;
        private float time;
        private float placeholder;

        public Image[] healthbar;
        public Image[] staminabar;
        public Image[] manabar;

        public float staminaFILLAMOUNT;
        public float healthFILLAMOUNT;
        public float manaFillAMOUNT;

        public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
        public float CurrentHealth { get { return _curHealth; } set { _curHealth = value; } }

        public float MaxStamina { get { return _maxStamina; } set { _maxStamina = value; } }
        public float CurrentStamina { get { return _curStamina; } set { _curStamina = value; } }
        public float StaminaRegenTime { get { return _staminaRegenTime; } set { _staminaRegenTime = value; } }

        public float MaxMana { get { return _maxMana; } set { _maxMana = value; } }
        public float CurrentMana { get { return _curMana; } set { _curMana = value; } }

        public int MinAttack { get { return _minAttack; } set { _minAttack = value; } }
        public int MaxAttack { get { return _maxAttack; } set { _maxAttack = value; } }

        public float HealthPotionHeal { get { return _healthPotionHeal; } set { _healthPotionHeal = value; } }
        public float ManaPotionHeal { get { return _manaPotionHeal; } set { _manaPotionHeal = value; } }

        public int HealthPotionAmount { get { return _healthPotionAmount; } set { _healthPotionAmount = value; } }
        public int ManaPotionAmount { get { return _manaPotionAmount; } set { _manaPotionAmount = value; } }
        public int BuffPotionAmount { get { return _buffPotionAmount; } set { _buffPotionAmount = value; } }
        public int MonsterMeatAmount { get { return (int)meatAmount; } set { meatAmount = value; } }




        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            playerLoco = GetComponent<PlayerLocomotion>();

            if (PlayerManager.pm == null)
            {
                PlayerManager.pm = this;
            }
            else if (PlayerManager.pm != this)
            {
                Destroy(this);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _am = AudioManager.instance;
            Debug.Log($"Starting ManaPotionAmount: {_manaPotionAmount}");
            //Sets current health and stamina to max
            _curHealth = _maxHealth;
            _curStamina = _maxStamina;
            _curMana = _maxMana;
            //sets the healthbar and staminabar to max
            foreach (Image bar in healthbar)
            {
                bar.fillAmount = _MAXFILLAMOUNT;
            }

            foreach (Image bar in staminabar)
            {
                bar.fillAmount = _MAXFILLAMOUNT;
            }

            foreach (Image bar in manabar)
            {
                bar.fillAmount = _MAXFILLAMOUNT;
            }

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

            if (CurrentHealth <= 0)
            {
                Die();
            }

            //Regens Stamina if taken stamina damage
            if (CurrentStamina != MaxStamina)
            {
                if (placeholder == 0)
                {
                    placeholder = CurrentStamina;
                }

                if (placeholder > CurrentStamina)
                {
                    time = 0;
                    placeholder = CurrentStamina;
                }

                time += Time.deltaTime;

                if (time >= StaminaRegenTime)
                {
                    CurrentStamina = MaxStamina;
                    time = 0;
                }

                //Debug.Log(time); Disabled by Patrick temporarily
            }

            GoldAmountTXT.text = "Gold: " + goldAmount;

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
            foreach (var bar in healthbar)
            {
                if (bar.fillAmount != DamageConversion(CurrentHealth, MaxHealth))
                {
                    bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(CurrentHealth, MaxHealth), 0.005f);
                }
                if (bar.fillAmount >= DamageConversion(CurrentHealth, MaxHealth) - 0.05)
                {
                    bar.fillAmount = DamageConversion(CurrentHealth, MaxHealth);
                }

            }

            foreach (var bar in staminabar)
            {
                if (bar.fillAmount != DamageConversion(CurrentStamina, MaxStamina))
                {
                    bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(CurrentStamina, MaxStamina), 0.005f);
                }

                if (bar.fillAmount >= DamageConversion(CurrentStamina, MaxStamina) - 0.05)
                {
                    bar.fillAmount = DamageConversion(CurrentStamina, MaxStamina);
                }

            }

            foreach (var bar in manabar)
            {
                if (bar.fillAmount != DamageConversion(CurrentMana, MaxMana))
                {
                    bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(CurrentMana, MaxMana), 0.005f);
                }
                if (bar.fillAmount >= DamageConversion(CurrentMana, MaxMana) - 0.05)
                {
                    bar.fillAmount = DamageConversion(CurrentMana, MaxMana);
                }
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

                if (resource != null)
                {
                    switch (resource.ResourceData.ResourceEnum)
                    {
                        case ResourceTypeEnum.MonsterMeat:
                            meatAmount += resource.ResourceData.GetAmount();
                            resource.PickupResource();
                            break;
                        case ResourceTypeEnum.Gold:
                            goldAmount += resource.ResourceData.GetAmount();
                            resource.PickupResource();
                            _am.PlaySFX(goldPickup);
                            break;
                        case ResourceTypeEnum.Health:
                            _healthPotionAmount += resource.ResourceData.GetAmount();
                            resource.PickupResource();
                            _am.PlaySFX(bottlePickup);
                            break;
                        case ResourceTypeEnum.Mana:
                            _manaPotionAmount += resource.ResourceData.GetAmount();
                            resource.PickupResource();
                            _am.PlaySFX(bottlePickup);
                            break;
                    }
                }


            }
        }

        public void GetHit(int damage)
        {
            if (isDead == false)                                               // If player is not dead,
            {
                CurrentHealth -= damage;                                     // Decrease health by 1

                if (CurrentHealth <= 0)                                      // Check for health less than or equal to 0
                {
                    isDead = true;                                         // dead bool = true
                }
            }
        }


        public void Die()
        {
            anim.SetTrigger("isDead");
            playerLoco.canMove = false;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;

            StartCoroutine(DestroyCoroutine());
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadSceneAsync(0);
        }

        public void GetStunned(float length)
        {
            return;
        }
    }
}
