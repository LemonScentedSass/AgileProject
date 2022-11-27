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
    // Start is called before the first frame update
    void Start()
    {
        hotbar = GetComponentInChildren<Hotbar>();

    }

    // Update is called once per frame
    void Update()
    {
        CurrentItem = hotbar.useItem;
        CurrentMagic = hotbar.useMagic;

        if (CurrentItem == null)
        {

            return;
        }
        else
        {
            hotbarItemImage.sprite = CurrentItem.itemIcon;
        }

        if (CurrentMagic == null)
        {
            return;
        }
        else
        {
            hotbarMagicImage.sprite = CurrentMagic.itemIcon;
        }



    }
}
