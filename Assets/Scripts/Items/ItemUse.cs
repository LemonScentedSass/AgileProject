using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemUse : MonoBehaviour
{
    //Move, this is just an example
    public float coolDown;

    public KeyCode HealthPotion = KeyCode.Alpha1;
    public KeyCode ManaPotion = KeyCode.Alpha2;
    public KeyCode Item = KeyCode.Q;
    public KeyCode Magic = KeyCode.E;

    //health potion
    public float HealthPotionCooldown = 5f;
    public float healthCooldownFILLAMOUNT;
    public TMP_Text healthPotionAmounTXT;

    public bool healthcooldown;
    public Image healthcooldownImage;

    public TMP_Text healthfullTXT;
    public bool healthfull;
    float healthfullFlashtime = 0;

    //mana potion
    public float ManaPotionCooldown = 5f;
    public float manaCooldownFILLAMOUNT;
    public TMP_Text manaPotionAmountTXT;

    public bool manacooldown;
    public Image manacooldownImage;

    public TMP_Text manafullTXT;
    public bool manafull;
    float manafullFlashTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthfullTXT.enabled = false;
        manafullTXT.enabled = false;
        if(healthcooldownImage.fillAmount != 0)
        {
            healthcooldownImage.fillAmount = 0;
        }
        if (manacooldownImage.fillAmount != 0)
        {
            manacooldownImage.fillAmount = 0;
        }

    }

    public IEnumerator StartCoolDown(float duration)
    {
        float t = 0;

        while(t < duration)
        {
            Debug.Log("trigger");
            t += Time.deltaTime;
            coolDown = t/duration;
            yield return null;
        }

        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");

        if (Input.GetKeyDown(HealthPotion))
        {
            StartCoroutine(StartCoolDown(2));
        }

        healthcooldownImage.fillAmount = coolDown;



        /*
        healthCooldownFILLAMOUNT = healthcooldownImage.fillAmount;
        healthPotionAmounTXT.text = "" + PlayerManagerUI.PlayerManager.pm.HealthPotionAmount;

        if (Input.GetKeyDown(HealthPotion) && PlayerManagerUI.PlayerManager.pm.HealthPotionAmount > 0 && healthcooldown == false)
        {

            if (PlayerManagerUI.PlayerManager.pm.CurrentHealth == PlayerManagerUI.PlayerManager.pm.MaxHealth)
            {
                healthfullFlashtime = 1f + Time.time;
                healthfull = true;
                return;
            }

            healthcooldownImage.fillAmount = 1;
            healthcooldown = true;
            if (PlayerManagerUI.PlayerManager.pm.HealthPotionAmount != 0)
            {
                UsePotion("Health");
            }

        }

        manaCooldownFILLAMOUNT = manacooldownImage.fillAmount;
        manaPotionAmountTXT.text = "" + PlayerManagerUI.PlayerManager.pm.ManaPotionAmount;

        if (Input.GetKeyDown(ManaPotion) && ManaPotion > 0 && manacooldown == false)
        {

            if (PlayerManagerUI.PlayerManager.pm.CurrentMana == PlayerManagerUI.PlayerManager.pm.MaxMana)
            {
                manafullFlashTime = 1f + Time.time;
                manafull = true;
                return;
            }

            manacooldownImage.fillAmount = 1;
            manacooldown = true;
            if (PlayerManagerUI.PlayerManager.pm.ManaPotionAmount != 0)
            {
                UsePotion("Mana");
            }

        }

        if (healthfull == true)
        {
            FlashisFull("Health");
        }
        else if(manafull == true)
        {
            FlashisFull("Mana");
        }


        if (healthcooldown == true)
        {
            healthcooldownImage.fillAmount = Mathf.Lerp(healthcooldownImage.fillAmount, 0, 0.001f * HealthPotionCooldown);
            if(healthcooldownImage.fillAmount <= 0.02f)
            {
                healthcooldownImage.fillAmount = 0;
                healthcooldown = false;
            }
        }

        if(manacooldown == true)
        {
            manacooldownImage.fillAmount = Mathf.Lerp(manacooldownImage.fillAmount, 0, 0.001f * ManaPotionCooldown);
            if (manacooldownImage.fillAmount <= 0.02f)
            {
                manacooldownImage.fillAmount = 0;
                manacooldown = false;
            }
        }
        */
    }

    private void UsePotion(string potionName)
    {
        if(potionName == "Health")
        {
            GameManager.PlayerManager.pm.healthRegen = true;
            GameManager.PlayerManager.pm.HealthPotionAmount -= 1;
        }
        if(potionName == "Mana")
        {
            GameManager.PlayerManager.pm.manaRegen = true;
            GameManager.PlayerManager.pm.ManaPotionAmount -= 1;
        }
      
    }

    private void FlashisFull(string potionName)
    {
        if(potionName == "Health")
        {
            healthfullTXT.enabled = true;
            if (Time.time > healthfullFlashtime)
            {
                healthfullTXT.enabled = false;
                healthfull = false;
            }

        }
        if(potionName == "Mana")
        {
            manafullTXT.enabled = true;
            if (Time.time > manafullFlashTime)
            {
                manafullTXT.enabled = false;
                manafull = false;
            }
        }
      

    }




}
