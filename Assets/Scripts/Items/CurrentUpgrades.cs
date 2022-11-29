using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using UnityEngine.UI;

public class CurrentUpgrades : MonoBehaviour
{
    public UseItem CurrentItem;
    public UseItem CurrentMagic;

    public Hotbar hotbar;

    public Image hotbarItemImage;
    public Image hotbarMagicImage;

    public Button menuItemUpgrade;
    public Button menuMagicUpgrade;

    public TMPro.TMP_Text itemLVLTXT;
    public TMPro.TMP_Text magicLVLTXT;

    public int CurrentItemLVL;
    public int CurrentMagicLVL;

    public TMPro.TMP_Text skillpointTXT;

    public int itemSkillRequirement = 1;
    public int magicSkillRequirement = 1;

    // Start is called before the first frame update
    void Start()
    {

        itemLVLTXT.text = "Lvl: " + CurrentItemLVL;
        magicLVLTXT.text = "Lvl: " + CurrentMagicLVL;

        hotbar = GetComponentInChildren<Hotbar>();
        CurrentMagic = hotbar.useMagic;
        CurrentItem = hotbar.useItem;

        Debug.Log(CurrentMagic);

        hotbar.useItem.itemLVL = 0;
        hotbar.useMagic.itemLVL = 0;
    }

    // Update is called once per frame
    void Update()
    {
        skillpointTXT.text = "Skill Points: " + LevelSystem.instance.skillPoints;
        itemLVLTXT.text = "Lvl: " + hotbar.useItem.itemLVL;

        SkillPointCheck();
        UpdateSkillRequirement();
        //Makes current
        if (CurrentMagic != hotbar.useMagic)
        {
            CurrentMagic = hotbar.useMagic;
        }

        if (CurrentItem != hotbar.useItem)
        {
            CurrentItem = hotbar.useItem;
        }

        //checks to make sure there is an item, if not put nothing image
        if (hotbar.useItem == null)
        {
            hotbarItemImage.sprite = null;
        }
        else
        {
            hotbarItemImage.sprite = hotbar.useItem.itemIcon;
        }

        if (hotbar.useMagic == null)
        {
            hotbarMagicImage.sprite = null;
        }
        else
        {
            hotbarMagicImage.sprite = hotbar.useMagic.itemIcon;
        }


    }

    public void IncreaseItemLevel()
    {
        if(hotbar.useItem.itemLVL == 0)
        {
            LevelSystem.instance.skillPoints -= itemSkillRequirement;
            hotbar.useItem.itemLVL += 1;
            CurrentItemLVL = hotbar.useItem.itemLVL;
            itemLVLTXT.text = "Lvl: " + hotbar.useItem.itemLVL;
        }
        else if(hotbar.useItem.itemLVL == 1)
        {
            LevelSystem.instance.skillPoints -= itemSkillRequirement;
            hotbar.useItem.itemLVL += 1;
            CurrentItemLVL = hotbar.useItem.itemLVL;
            itemLVLTXT.text = "Lvl: " + hotbar.useItem.itemLVL;
        }
        else if (hotbar.useItem.itemLVL == 2)
        {
            LevelSystem.instance.skillPoints -= itemSkillRequirement;
            hotbar.useItem.itemLVL += 1;
            CurrentItemLVL = hotbar.useItem.itemLVL;
            itemLVLTXT.text = "Lvl: " + hotbar.useItem.itemLVL;
        }
    }

    public void IncreaseMagicLevel()
    {

        if (hotbar.useMagic.itemLVL == 0)
        {
            LevelSystem.instance.skillPoints -= magicSkillRequirement;
            hotbar.useMagic.itemLVL += 1;
            CurrentMagicLVL = hotbar.useMagic.itemLVL;
            magicLVLTXT.text = "Lvl: " + hotbar.useMagic.itemLVL;
        }
        else if (hotbar.useMagic.itemLVL == 1)
        {
            LevelSystem.instance.skillPoints -= magicSkillRequirement;
            hotbar.useMagic.itemLVL += 1;
            CurrentMagicLVL = hotbar.useMagic.itemLVL;
            magicLVLTXT.text = "Lvl: " + hotbar.useMagic.itemLVL;
        }
        else if (hotbar.useMagic.itemLVL == 2)
        {
            LevelSystem.instance.skillPoints -= magicSkillRequirement;
            hotbar.useMagic.itemLVL += 1;
            CurrentMagicLVL = hotbar.useMagic.itemLVL;
            magicLVLTXT.text = "Lvl: " + hotbar.useMagic.itemLVL;
        }
    }

    private void SkillPointCheck()
    {

        if (itemSkillRequirement <= LevelSystem.instance.skillPoints)
        {
            menuItemUpgrade.interactable = true;
        }
        else
        {
            menuItemUpgrade.interactable = false;
        }

        if (magicSkillRequirement <= LevelSystem.instance.skillPoints)
        {
            menuMagicUpgrade.interactable = true;
        }
        else
        {
            menuMagicUpgrade.interactable = false;
        }

    }

    private void UpdateSkillRequirement()
    {

        if (hotbar.useItem.itemLVL == 0)
        {
            itemSkillRequirement = 1;
        }
        else if (hotbar.useItem.itemLVL == 1)
        {
            itemSkillRequirement = 3;
        }
        else if (hotbar.useItem.itemLVL == 2)
        {
            itemSkillRequirement = 5;
        }

        if (hotbar.useMagic.itemLVL == 0)
        {
            magicSkillRequirement = 1;
        }
        else if (hotbar.useMagic.itemLVL == 1)
        {
            magicSkillRequirement = 3;
        }
        else if (hotbar.useMagic.itemLVL == 2)
        {
            magicSkillRequirement = 5;
        }

        if (hotbar.useItem.itemLVL != 3)
        {
            menuItemUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Skill Points: " + itemSkillRequirement;
        }
        else
        {
            menuItemUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Max";
        }

        if (hotbar.useMagic.itemLVL != 3)
        {
            menuMagicUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Skill Points: " + magicSkillRequirement;
        }
        else
        {
            menuMagicUpgrade.GetComponentInChildren<TMPro.TMP_Text>().text = "Max";
        }


    }

}
