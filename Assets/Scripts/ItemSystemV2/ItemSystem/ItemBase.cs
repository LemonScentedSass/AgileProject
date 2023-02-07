using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemV2
{
      public abstract class ItemBase : ScriptableObject
      {
            // Properties that all items in the game share
            public string itemName;
            public string itemDescription;
            public int itemCost;
            public Sprite itemImage;
      }
}

