using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC/Module")]
    public class Module : ScriptableObject
    {
        public TileBase tilebase;
        public Sprite image;
        public Module filledTile; // The sprite of the tile that this module will be swapped to at the end of generation

        public Module uncoloredTwin; // Version of this module without the color block
        [Range(0, 100)] public float chanceToSpawn;

        public Module[] north;
        public Module[] east;
        public Module[] south;
        public Module[] west;

        public Module[] detailModule;
    }
}
