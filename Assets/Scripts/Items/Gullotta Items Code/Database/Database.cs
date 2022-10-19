using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStorage
{
    [CreateAssetMenu(menuName = "Database")]
    public class Database : ScriptableObject
    {
        public DatabaseElement[] elements;

        public void SetIndexes()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].SetIndex(i);
            }
        }
    }
}