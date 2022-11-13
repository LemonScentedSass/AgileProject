using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapGeneration
{
    public class GenerationStages : MonoBehaviour
    {
        public int roomsGenerated = 0;
        public int curWFCRooms = 0;
        public bool startEndFound = false;

        private bool spaceManagerCalled = false;

        public Image loadingScreen;

        public static GenerationStages instance;

        void Start()
        {
            if (GenerationStages.instance == null)
                GenerationStages.instance = this;
            if (GenerationStages.instance != this)
                Destroy(this);
        }

        void Update()
        {
            if (roomsGenerated > 0 && curWFCRooms == 0 && spaceManagerCalled == false)
            {
                Debug.Log("Ready for SpaceManager @ " + Time.time);
                SpaceManager.instance.CalculateSpace();
                spaceManagerCalled = true;
            }
        }
    }
}