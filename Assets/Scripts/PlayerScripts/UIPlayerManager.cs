using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;

public class UIPlayerManager : MonoBehaviour
{
    //Insert all UI StatBars that track player stats
    [SerializeField] private Image[] healthbar;
    [SerializeField] private Image[] staminabar;
    [SerializeField] private Image[] manabar;

    [SerializeField] private float staminaFILLAMOUNT;
    [SerializeField] private float healthFILLAMOUNT;
    [SerializeField] private float manaFillAMOUNT;

    [SerializeField] private float _MAXFILLAMOUNT = 1.0f;

    [SerializeField] private TMPro.TMP_Text GoldAmountTXT;

    // Start is called before the first frame update
    void Start()
    {
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
        staminaFILLAMOUNT = DamageConversion(PlayerManager.pm.CurrentStamina, PlayerManager.pm.MaxStamina);
        healthFILLAMOUNT = DamageConversion(PlayerManager.pm.CurrentHealth, PlayerManager.pm.MaxHealth);
        manaFillAMOUNT = DamageConversion(PlayerManager.pm.CurrentMana, PlayerManager.pm.MaxMana);


        DisplayStatConversion(); //Converts health, stamina, and mana into fillamount for health, stamina, and mana bars
        StatsCheck(); // Checks to make sure numbers dont go below or above 0 and 100

        GoldAmountTXT.text = "Gold: " + PlayerManager.pm.goldAmount;
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
        if (PlayerManager.pm.CurrentHealth > PlayerManager.pm.MaxHealth)
        {
            PlayerManager.pm.CurrentHealth = PlayerManager.pm.MaxHealth;
        }
        if (PlayerManager.pm.CurrentHealth < 0)
        {
            PlayerManager.pm.CurrentHealth = 0;
        }

        //Makes sure stamina doesn't go over max and under 0
        if (PlayerManager.pm.CurrentStamina > PlayerManager.pm.MaxStamina)
        {
            PlayerManager.pm.CurrentStamina = PlayerManager.pm.MaxStamina;
        }
        if (PlayerManager.pm.CurrentStamina < 0)
        {
            PlayerManager.pm.CurrentStamina = 0;
        }

        //Makes sure mana doesn't go over max and under 0
        if (PlayerManager.pm.CurrentMana > PlayerManager.pm.MaxMana)
        {
            PlayerManager.pm.CurrentMana = PlayerManager.pm.MaxMana;
        }
        if (PlayerManager.pm.CurrentMana < 0)
        {
            PlayerManager.pm.CurrentMana = 0;
        }
    }

    private void DisplayStatConversion()
    {
        //converts amount and fluidly change health with lerp
        foreach (var bar in healthbar)
        {
            if (bar.fillAmount != DamageConversion(PlayerManager.pm.CurrentHealth, PlayerManager.pm.MaxHealth))
            {
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(PlayerManager.pm.CurrentHealth, PlayerManager.pm.MaxHealth), 0.005f);
            }
            if (bar.fillAmount >= DamageConversion(PlayerManager.pm.CurrentHealth, PlayerManager.pm.MaxHealth) - 0.05)
            {
                bar.fillAmount = DamageConversion(PlayerManager.pm.CurrentHealth, PlayerManager.pm.MaxHealth);
            }

        }

        foreach (var bar in staminabar)
        {
            if (bar.fillAmount != DamageConversion(PlayerManager.pm.CurrentStamina, PlayerManager.pm.MaxStamina))
            {
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(PlayerManager.pm.CurrentStamina, PlayerManager.pm.MaxStamina), 0.005f);
            }

            if (bar.fillAmount >= DamageConversion(PlayerManager.pm.CurrentStamina, PlayerManager.pm.MaxStamina) - 0.05)
            {
                bar.fillAmount = DamageConversion(PlayerManager.pm.CurrentStamina, PlayerManager.pm.MaxStamina);
            }

        }

        foreach (var bar in manabar)
        {
            if (bar.fillAmount != DamageConversion(PlayerManager.pm.CurrentMana, PlayerManager.pm.MaxMana))
            {
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, DamageConversion(PlayerManager.pm.CurrentMana, PlayerManager.pm.MaxMana), 0.005f);
            }
            if (bar.fillAmount >= DamageConversion(PlayerManager.pm.CurrentMana, PlayerManager.pm.MaxMana) - 0.05)
            {
                bar.fillAmount = DamageConversion(PlayerManager.pm.CurrentMana, PlayerManager.pm.MaxMana);
            }
        }

    }

}
