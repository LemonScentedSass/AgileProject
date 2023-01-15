using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotbar : MonoBehaviour
{
    //NOTHING IS SET (Delete when set)
    public Image magiccooldownImage;
    public Image itemcooldownImage;

    public TMP_Text healthPotionAmounTXT;
    public Image healthcooldownImage;
    public Button healthPotionButton;

    public TMP_Text manaPotionAmountTXT;
    public Image manacooldownImage;
    public Button manaPotionButton;

    public TMP_Text buffPotionAmountTXT;
    public Image buffcooldownImage;
    public Button buffPotionButton;

    public TMP_Text meatAmountTXT;
    public Image meatcooldownImage;
    public Button monsterMeatButton;
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
