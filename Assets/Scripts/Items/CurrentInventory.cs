using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using UnityEngine.UI;

public class CurrentInventory : MonoBehaviour
{
    public UseItem CurrentItem;
    public UseItem CurrentMagic;

    private Hotbar hotbar;

    public Image hotbarItemImage;
    public Image hotbarMagicImage;

    public int CurrentItemLVL;
    public int CurrentMagicLVL;

    public TMPro.TMP_Text skillpointTXT;

    // Start is called before the first frame update
    void Start()
    {
        hotbar = GetComponentInChildren<Hotbar>();
        CurrentMagic = hotbar.useMagic;
        CurrentItem = hotbar.useItem;
    }

    // Update is called once per frame
    void Update()
    {
        skillpointTXT.text = "Skill Points: " + LevelSystem.instance.skillPoints;

        if(CurrentMagic != hotbar.useMagic)
        {
            CurrentMagic = hotbar.useMagic;
            CurrentMagicLVL = hotbar.useMagic.itemLVL;
        }

        if (CurrentItem != hotbar.useItem)
        {
            CurrentItem = hotbar.useItem;
            CurrentItemLVL = hotbar.useItem.itemLVL;
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
}
