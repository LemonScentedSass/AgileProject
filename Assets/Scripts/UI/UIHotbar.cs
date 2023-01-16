using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UIManager
{
    public class UIHotbar : MonoBehaviour
    {
        public static UIHotbar hotbarUI;

        [Header("Item/Magic Cooldown UI")]
        public Image magiccooldownImage;
        public Image itemcooldownImage;

        [Header("Health Potion UI")]
        public TMP_Text healthPotionAmounTXT;
        public Image healthcooldownImage;
        public Button healthPotionButton;

        [Header("Mana Potion UI")]
        public TMP_Text manaPotionAmountTXT;
        public Image manacooldownImage;
        public Button manaPotionButton;

        [Header("Buff Potion UI")]
        public TMP_Text buffPotionAmountTXT;
        public Image buffcooldownImage;
        public Button buffPotionButton;

        [Header("Meat UI")]
        public TMP_Text meatAmountTXT;
        public Image meatcooldownImage;
        public Button monsterMeatButton;

        private void Awake()
        {
            if(hotbarUI == null)
            {
                hotbarUI = this;
            }
            else if(hotbarUI != this)
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //Sets hotbar UI amount
            healthPotionAmounTXT.text = "x" + GameManager.PlayerManager.pm.HealthPotionAmount;
            manaPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.ManaPotionAmount;
            buffPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.BuffPotionAmount;
            meatAmountTXT.text = "x" + GameManager.PlayerManager.pm.MonsterMeatAmount;

            //Checks that cooldown is not active at start
            if (healthcooldownImage.fillAmount != 0)
            {
                healthcooldownImage.fillAmount = 0;
            }
            if (manacooldownImage.fillAmount != 0)
            {
                manacooldownImage.fillAmount = 0;
            }

        }

        // Update is called once per frame
        void Update()
        {
            PotionAmountText();
        }

        public void PotionAmountText()
        {
            if (healthPotionAmounTXT.text != "x" + GameManager.PlayerManager.pm.HealthPotionAmount)
            {
                healthPotionAmounTXT.text = "x" + GameManager.PlayerManager.pm.HealthPotionAmount;
            }
            if (manaPotionAmountTXT.text != "x" + GameManager.PlayerManager.pm.ManaPotionAmount)
            {
                manaPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.ManaPotionAmount;
            }
            if (buffPotionAmountTXT.text != "x" + GameManager.PlayerManager.pm.BuffPotionAmount)
            {
                buffPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.BuffPotionAmount;
            }
            if (meatAmountTXT.text != "x" + GameManager.PlayerManager.pm.MonsterMeatAmount)
            {
                meatAmountTXT.text = "x" + GameManager.PlayerManager.pm.MonsterMeatAmount;
            }
        }
    }
}


