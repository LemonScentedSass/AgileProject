using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ItemSystem;


public class Hotbar : MonoBehaviour
{
      
      private Animator anim;

      [Header("Hotbar Keys")]
      public KeyCode HealthPotion = KeyCode.Alpha1;
      public KeyCode ManaPotion = KeyCode.Alpha2;
      public KeyCode BuffPotion = KeyCode.Alpha3;
      public KeyCode MonsterMeat = KeyCode.Alpha4;
      public KeyCode Item = KeyCode.Mouse1;
      public KeyCode Magic = KeyCode.Q;


      [Header("Current Magic")]
      public float MagicCooldownDuration = 5f;
      public Image magiccooldownImage;
      public UseItem useMagic;

      [Header("Current Item")]
      public float ItemCooldownDuration = 5f;
      public Image itemcooldownImage;
      public UseItem useItem;

      [Header("Health Potion")]
      public float HealthPotionCooldownDuration = 5f;
      public TMP_Text healthPotionAmounTXT;
      public Image healthcooldownImage;
      public Button healthPotionButton;
      public UseItem useHealthPotion;

      [Header("Mana Potion")]
      //mana potion
      public float ManaPotionCooldownDuration = 5f;
      public TMP_Text manaPotionAmountTXT;
      public Image manacooldownImage;
      public Button manaPotionButton;
      public UseItem useManaPotion;

      [Header("Buff Potion")]
      //buff potion
      public float BuffPotionCooldownDuration = 5f;
      public TMP_Text buffPotionAmountTXT;
      public Image buffcooldownImage;
      public Button buffPotionButton;
      public UseItem useBuffPotion;

      [Header("Monster Meat")]
      //monster meat
      public float MeatCooldownDuration = 5f;
      public TMP_Text meatAmountTXT;
      public Image meatcooldownImage;
      public Button monsterMeatButton;
      public UseItem useMonsterMeat;

      private float ResetCooldown;

      // Start is called before the first frame update
      void Start()
      {
            anim = GetComponent<Animator>();
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
            //Checks player input for magic key; Checks to make sure player is not on cooldown or using item
            if (Input.GetKeyDown(Magic) && GameManager.PlayerManager.pm.usingItem == false && magiccooldownImage.fillAmount == 0)
            {
                //Plays specific fireball animation in layer
                  anim.Play("Fireball", 2);

                //Uses item and starts cooldown
                  GameManager.PlayerManager.pm.usingItem = true;
                  GameManager.PlayerManager.pm.CurrentMana -= 10f;
                  StartCoroutine(StartCoolDown(MagicCooldownDuration, magiccooldownImage));
            }

            //Checks player input for item key; 
            if (Input.GetKeyDown(Item) && GameManager.PlayerManager.pm.usingItem == false && itemcooldownImage.fillAmount == 0)
            {


                  if (useItem.itemName == "Bow")
                  {
                      anim.Play("Bow", 1);
                  }
                  else
                  {
                      anim.Play("Boomerang", 1);
                  }



                  GameManager.PlayerManager.pm.usingItem = true;
                  GameManager.PlayerManager.pm.CurrentStamina -= 15f;
                  StartCoroutine(StartCoolDown(ItemCooldownDuration, itemcooldownImage));
            }


            PotionUse();
            PotionAmountText();
      }

    //Uses current item
      public void UseItem()
      {
            useItem.OnUseItem(GameManager.PlayerManager.pm.transform);
      }

    //Uses current magic
      public void UseMagic()
      {
            useMagic.OnUseItem(GameManager.PlayerManager.pm.transform);
      }

    //Uses potion on hotbar key press
      private void PotionUse()
      {
            //Health Potion
            if (Input.GetKeyUp(HealthPotion))
            {
                  useHealthPotion.OnUseItem(GameManager.PlayerManager.pm.transform);
                  HealthPotionCooldown();

            }
            //Mana Potion
            if (Input.GetKeyUp(ManaPotion))
            {
                  useManaPotion.OnUseItem(GameManager.PlayerManager.pm.transform);
                  ManaPotionCooldown();
            }
            //Buff Potion
            if (Input.GetKeyUp(BuffPotion))
            {
                  useBuffPotion.OnUseItem(GameManager.PlayerManager.pm.transform);
                  BuffPotionCooldown();
            }
            //Monster Meat
            if (Input.GetKeyUp(MonsterMeat))
            {
                  useMonsterMeat.OnUseItem(GameManager.PlayerManager.pm.transform);
                  MonsterMeatCooldown();
            }
      }


      private void PotionAmountText()
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

      public void HealthPotionCooldown()
      {
            if (GameManager.PlayerManager.pm.HealthPotionAmount != 0 && healthcooldownImage.fillAmount == 0)
            {
                  healthPotionButton.interactable = false;
                  StartCoroutine(StartCoolDown(HealthPotionCooldownDuration, healthcooldownImage, healthPotionButton));
                  GameManager.PlayerManager.pm.HealthPotionAmount -= 1;
                  healthPotionAmounTXT.text = "x" + GameManager.PlayerManager.pm.HealthPotionAmount;
            }

      }
      public void ManaPotionCooldown()
      {
            if (GameManager.PlayerManager.pm.ManaPotionAmount != 0 && manacooldownImage.fillAmount == 0)
            {
                  manaPotionButton.interactable = false;
                  StartCoroutine(StartCoolDown(ManaPotionCooldownDuration, manacooldownImage, manaPotionButton));
                  GameManager.PlayerManager.pm.ManaPotionAmount -= 1;
                  manaPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.ManaPotionAmount;
            }

      }

      public void BuffPotionCooldown()
      {
            if (GameManager.PlayerManager.pm.BuffPotionAmount != 0 && buffcooldownImage.fillAmount == 0)
            {
                  buffPotionButton.interactable = false;
                  StartCoroutine(StartCoolDown(BuffPotionCooldownDuration, buffcooldownImage, buffPotionButton));
                  GameManager.PlayerManager.pm.BuffPotionAmount -= 1;
                  buffPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.BuffPotionAmount;
            }

      }

      public void MonsterMeatCooldown()
      {
            if (GameManager.PlayerManager.pm.MonsterMeatAmount != 0 && meatcooldownImage.fillAmount == 0)
            {
                  monsterMeatButton.interactable = false;
                  StartCoroutine(StartCoolDown(MeatCooldownDuration, meatcooldownImage, monsterMeatButton));
                  GameManager.PlayerManager.pm.MonsterMeatAmount -= 1;
                  meatAmountTXT.text = "x" + GameManager.PlayerManager.pm.MonsterMeatAmount;
            }

      }

    //Starts cooldown for specific hotbar buttons and its cooldown 
    //Disables hotbar button press
      public IEnumerator StartCoolDown(float duration, Image fill, Button button)
      {
            GameManager.PlayerManager.pm.usingItem = false;
            float t = 0;

            while (t < duration)
            {
                  Debug.Log("trigger");
                  t += Time.deltaTime;
                  float reset = t / duration;
                  fill.fillAmount = reset;
                  yield return null;
            }

            button.interactable = true;
            fill.fillAmount = 0;
      }


    //Starts cooldown for specific hotbar and its cooldown
      public IEnumerator StartCoolDown(float duration, Image fill)
      {
            GameManager.PlayerManager.pm.usingItem = false;
            float t = 0;

            while (t < duration)
            {
                  t += Time.deltaTime;
                  float reset = t / duration;
                  fill.fillAmount = reset;
                  yield return null;
            }

            fill.fillAmount = 0;
      }

}
