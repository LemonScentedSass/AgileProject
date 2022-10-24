using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerManager;
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
            EXPtext.text = "EXP: " + PlayerManager.PlayerManager.pm.CurrentEXP + "/" + PlayerManager.PlayerManager.pm.MaxEXP;
        }

        foreach (var levelTXT in Level)
        {
            levelTXT.text = "Level: " + PlayerManager.PlayerManager.pm.PlayerLevel;
        }
        Health.text = "Health: " + PlayerManager.PlayerManager.pm.CurrentHealth + "/" + PlayerManager.PlayerManager.pm.MaxHealth;
        Stamina.text = "Stamina: " + PlayerManager.PlayerManager.pm.CurrentStamina + "/" + PlayerManager.PlayerManager.pm.MaxStamina;
        Mana.text = "Mana: " + PlayerManager.PlayerManager.pm.CurrentMana + "/" + PlayerManager.PlayerManager.pm.MaxMana;
        AttackDamage.text = "Attack Damage: " + PlayerManager.PlayerManager.pm.MinAttack + "-" + PlayerManager.PlayerManager.pm.MaxAttack;
        HealthPotion.text = "Heal: " + PlayerManager.PlayerManager.pm.HealthPotionHeal;
        ManaPotion.text = "Mana Heal: " + PlayerManager.PlayerManager.pm.ManaPotionHeal;
    }
   
}
