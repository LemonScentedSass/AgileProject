using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

public class CharacterTest : MonoBehaviour
{
    public UseItem useItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            useItem.OnUseItem(transform);
        }
    }
}
