using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerManagerUI;
using TMPro;

public class StatsScript : MonoBehaviour
{
    public TMP_Text[] EXP;
    public TMP_Text[] Level;
    public TMP_Text Health;
    public TMP_Text Stamina;
    public TMP_Text Mana;
    public TMP_Text AttackDamage;
    public TMP_Text HealthPotion;
    public TMP_Text ManaPotion;

    private void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        foreach (var EXPtext in EXP)
        {
            EXPtext.text = "EXP: " + PlayerManager.pm.CurrentEXP + "/" + PlayerManager.pm.MaxEXP;
        }

        foreach (var levelTXT in Level)
        {
            levelTXT.text = "Level: " + PlayerManager.pm.PlayerLevel;
        }
        Health.text = "Health: " + PlayerManager.pm.CurrentHealth + "/" + PlayerManager.pm.MaxHealth;
        Stamina.text = "Stamina: " + PlayerManager.pm.CurrentStamina + "/" + PlayerManager.pm.MaxStamina;
        Mana.text = "Mana: " + PlayerManager.pm.CurrentMana + "/" + PlayerManager.pm.MaxMana;
        AttackDamage.text = "Attack Damage: " + PlayerManager.pm.MinAttack + "-" + PlayerManager.pm.MaxAttack;
        HealthPotion.text = "Heal: " + PlayerManager.pm.HealthPotionHeal;
        ManaPotion.text = "Mana Heal: " + PlayerManager.pm.ManaPotionHeal;
    }
   
}
