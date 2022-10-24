using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EffectSystem;

[CreateAssetMenu(menuName = "Item Effect/Potion")]
public class StatEffect : EffectBase
{
    public int amount;
    public StatType statType;

    public override void UseEffect(Transform user)
    {
        //Make this better
        if(user.gameObject.GetComponent<GameManager.PlayerManager>() == null)
        {
            return;
        }



        //make better as well, enemies might want this effect as well
        GameManager.PlayerManager theSUser = user.GetComponent<GameManager.PlayerManager>();

        switch (statType)
        {
            case StatType.Health:
                theSUser.CurrentHealth += amount;
                break;
            case StatType.Mana:
                theSUser.CurrentMana += amount;
                break;
            case StatType.Stamina:
                theSUser.CurrentStamina += amount;
                break;
        }
    }

    public enum StatType
    {
        Health,
        Mana,
        Stamina
    }
}
