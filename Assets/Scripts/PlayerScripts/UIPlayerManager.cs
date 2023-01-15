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

    public TMPro.TMP_Text[] LevelTXT;                                 //All level texts in game
    public TMPro.TMP_Text[] ExpTXT;                                   //All Exp texts in game
    public Image[] ExpSlider;                                         //All Exp sliders in game

    public Image hotbarItemImage;
    public Image hotbarMagicImage;

    public Button menuItemUpgrade;
    public Button menuMagicUpgrade;

    public TMPro.TMP_Text itemLVLTXT;
    public TMPro.TMP_Text magicLVLTXT;

    public TMPro.TMP_Text skillpointTXT;


    // Start is called before the first frame update
    void Start()
    {
        itemLVLTXT.text = "Lvl: " + PlayerManager.pm.GetComponent<CurrentUpgrades>().CurrentItemLVL;
        magicLVLTXT.text = "Lvl: " + PlayerManager.pm.GetComponent<CurrentUpgrades>().CurrentMagicLVL;

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

        UpdateSlidertxt(); //Updates all sliders to current stat

        skillpointTXT.text = "Skill Points: " + LevelSystem.instance.skillPoints;
        itemLVLTXT.text = "Lvl: " + PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useItem.itemLVL;

        //checks to make sure there is an item, if not put nothing image
        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useItem == null)
        {
            hotbarItemImage.sprite = null;
        }
        else
        {
            hotbarItemImage.sprite = PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useItem.itemIcon;
        }

        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useMagic == null)
        {
            hotbarMagicImage.sprite = null;
        }
        else
        {
            hotbarMagicImage.sprite = PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useMagic.itemIcon;
        }


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

    //MAIN STATS slider conversion
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

    private void UpdateLVLtxt() //Updates all text UI
    {
        for (int i = 0; i < LevelTXT.Length; i++)
        {
            LevelTXT[i].text = "Level: " + GameManager.PlayerManager.pm.GetComponent<LevelSystem>().level;
        }

        for (int i = 0; i < ExpTXT.Length; i++)
        {
            ExpTXT[i].text = "EXP: " + GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experience + "/" + GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experienceToNextLevel;
        }
    }

    private void UpdateSlidertxt()
    {
        //Grabs each slider and checks if slider equals current stat, if not increase stat to current
        foreach (var slider in ExpSlider)
        {
            if (slider.fillAmount != (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experience / (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experienceToNextLevel)
            {
                slider.fillAmount = Mathf.Lerp(slider.fillAmount,
                    ((float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experience / (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experienceToNextLevel), 0.005f);
            }

            if (slider.fillAmount >= (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experience / (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experienceToNextLevel - 0.005)
            {
                slider.fillAmount = (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experience / (float)GameManager.PlayerManager.pm.GetComponent<LevelSystem>().experienceToNextLevel;
            }
        }

    }

    private void UpdateUpgradeSkillRequirement()
    {
        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useItem.itemLVL != 3)
        {
            menuItemUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Skill Points: " + PlayerManager.pm.GetComponent<CurrentUpgrades>().itemSkillRequirement;
        }
        else
        {
            menuItemUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Max";
        }

        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().hotbar.useMagic.itemLVL != 3)
        {
            menuMagicUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Skill Points: " + PlayerManager.pm.GetComponent<CurrentUpgrades>().magicSkillRequirement;
        }
        else
        {
            menuMagicUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Max";
        }

    }

    private void SkillPointCheck()
    {

        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().itemSkillRequirement <= LevelSystem.instance.skillPoints)
        {
            menuItemUpgrade.interactable = true;
        }
        else
        {
            menuItemUpgrade.interactable = false;
        }

        if (PlayerManager.pm.GetComponent<CurrentUpgrades>().magicSkillRequirement <= LevelSystem.instance.skillPoints)
        {
            menuMagicUpgrade.interactable = true;
        }
        else
        {
            menuMagicUpgrade.interactable = false;
        }

    }


}
