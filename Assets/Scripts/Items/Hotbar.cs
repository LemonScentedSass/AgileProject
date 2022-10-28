using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hotbar : MonoBehaviour
{
    private float ResetCooldown;

    [Header("Hotbar Keys")]
    public KeyCode HealthPotion = KeyCode.Alpha1;
    public KeyCode ManaPotion = KeyCode.Alpha2;
    public KeyCode BuffPotion = KeyCode.Alpha3;
    public KeyCode MonsterMeat = KeyCode.Alpha4;
    public KeyCode Item = KeyCode.Q;
    public KeyCode Magic = KeyCode.E;

    [Header("Health Potion")]
    public float HealthPotionCooldownDuration = 5f;
    public TMP_Text healthPotionAmounTXT;
    public Image healthcooldownImage;

    [Header("Mana Potion")]
    //mana potion
    public float ManaPotionCooldownDuration = 5f;
    public TMP_Text manaPotionAmountTXT;
    public Image manacooldownImage;

    [Header("Buff Potion")]
    //buff potion
    public float BuffPotionCooldownDuration = 5f;
    public TMP_Text buffPotionAmountTXT;
    public Image buffcooldownImage;

    [Header("Monster Meat")]
    //monster meat
    public float MeatCooldownDuration = 5f;
    public TMP_Text meatAmountTXT;
    public Image meatcooldownImage;

    // Start is called before the first frame update
    void Start()
    {
        healthPotionAmounTXT.text = "x" + GameManager.PlayerManager.pm.HealthPotionAmount;
        manaPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.ManaPotionAmount;
        buffPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.BuffPotionAmount;
        meatAmountTXT.text = "x" + GameManager.PlayerManager.pm.MonsterMeatAmount;

        if (healthcooldownImage.fillAmount != 0)
        {
            healthcooldownImage.fillAmount = 0;
        }
        if (manacooldownImage.fillAmount != 0)
        {
            manacooldownImage.fillAmount = 0;
        }

    }

    public IEnumerator StartCoolDown(float duration, float reset, Image fill)
    {
        float t = 0;

        while(t < duration)
        {
            Debug.Log("trigger");
            Debug.Log(reset);
            t += Time.deltaTime;
            reset = t/duration;
            fill.fillAmount = reset;
            yield return null;
        }

        CoolDownCheck(fill);
    }
    private void CoolDownCheck(Image fill)
    {
        fill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PotionUse();
        PotionAmountText();
    }


    private void PotionUse()
    {
        //Health Potion
        if (Input.GetKeyUp(HealthPotion) && GameManager.PlayerManager.pm.HealthPotionAmount != 0 && healthcooldownImage.fillAmount == 0)
        {
            StartCoroutine(StartCoolDown(HealthPotionCooldownDuration, ResetCooldown, healthcooldownImage));
            GameManager.PlayerManager.pm.HealthPotionAmount -= 1;
            healthPotionAmounTXT.text = "x" + GameManager.PlayerManager.pm.HealthPotionAmount;

        }
        //Mana Potion
        if (Input.GetKeyUp(ManaPotion) && GameManager.PlayerManager.pm.ManaPotionAmount != 0 && manacooldownImage.fillAmount == 0)
        {
            StartCoroutine(StartCoolDown(ManaPotionCooldownDuration, ResetCooldown, manacooldownImage));
            GameManager.PlayerManager.pm.ManaPotionAmount -= 1;
            manaPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.ManaPotionAmount;
        }
        //Buff Potion
        if (Input.GetKeyUp(BuffPotion) && GameManager.PlayerManager.pm.BuffPotionAmount != 0 && buffcooldownImage.fillAmount == 0)
        {
            StartCoroutine(StartCoolDown(BuffPotionCooldownDuration, ResetCooldown, buffcooldownImage));
            GameManager.PlayerManager.pm.BuffPotionAmount -= 1;
            buffPotionAmountTXT.text = "x" + GameManager.PlayerManager.pm.BuffPotionAmount;
        }
        //Monster Meat
        if (Input.GetKeyUp(MonsterMeat) && GameManager.PlayerManager.pm.MonsterMeatAmount != 0 && meatcooldownImage.fillAmount == 0)
        {
            StartCoroutine(StartCoolDown(BuffPotionCooldownDuration, ResetCooldown, meatcooldownImage));
            GameManager.PlayerManager.pm.MonsterMeatAmount -= 1;
            meatAmountTXT.text = "x" + GameManager.PlayerManager.pm.MonsterMeatAmount;
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

}
