using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionUse : MonoBehaviour
{
    public KeyCode keyPress = KeyCode.Alpha1;

    public Image PotionFill;
    public float PotionFILLAMOUNT;
    public float cooldownFILLAMOUNT;
    public int potionAmount = 0;
    public TMP_Text potionAmountText;

    public bool cooldown;
    public Image cooldownImage;
    public float cooldownTime = 3f;

    public PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        if(cooldownImage.fillAmount != 0)
        {
            cooldownImage.fillAmount = 0;
        }

        if(potionAmount == 0)
        {
            potionAmount = 3;

        }
        PotionFill.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

        PotionFILLAMOUNT = PotionFill.fillAmount;
        cooldownFILLAMOUNT = cooldownImage.fillAmount;
        potionAmountText.text = "" + potionAmount;

        if (Input.GetKeyDown(keyPress) && potionAmount > 0 && cooldown == false)
        {
            PotionFILLAMOUNT = 0;
            cooldownImage.fillAmount = 1;
            cooldown = true;
            if (potionAmount != 0)
            {
                UsePotion();
            }

        }

        if(cooldown == true)
        {
            cooldownImage.fillAmount = Mathf.Lerp(cooldownImage.fillAmount, 0, 0.005f);
            PotionFILLAMOUNT = 1;
            if(cooldownImage.fillAmount <= 0.02f)
            {
                cooldownImage.fillAmount = 0;
                cooldown = false;
            }
        }


    }

    private void UsePotion()
    {
        pm.healthRegen = true;
        potionAmount -= 1;
    }
}
