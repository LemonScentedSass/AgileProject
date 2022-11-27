using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

public class CurrentInventory : MonoBehaviour
{
    public UseItem CurrentItem;
    public UseItem CurrentMagic;

    private Hotbar hotbar;

    public int CurrentItemLVL;
    public int CurrentMagicLVL;
    // Start is called before the first frame update
    void Start()
    {
        hotbar = GetComponentInChildren<Hotbar>();
        CurrentItem = hotbar.useItem;
        CurrentMagic = hotbar.useMagic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
