using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGeneration;

namespace LevelData
{
    public class CreatePillars : MonoBehaviour
    {
        public static CreatePillars instance;

        public GameObject pillarPrefabEast;
        public GameObject pillarPrefabNorth;
        public GameObject pillarPrefabSouth;
        public GameObject pillarPrefabWest;
        public Transform pillarSpawner;
        public int pillarsX = 2;
        public int pillarsY = 2;

        private void Awake()
        {
            if (CreatePillars.instance == null)
                CreatePillars.instance = this;
            if (CreatePillars.instance != this)
                Destroy(this);
        }

        public void CreateRoomPillars(List<Room> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                Room curRoom = rooms[i];

                if (curRoom.TurnedOff == false)
                {
                    Vector2 curRoomTopRight = new Vector2(curRoom.Position.x + curRoom.GetWidth, curRoom.Position.y + curRoom.GetHeight);
                    Vector2 curRoomBottomLeft = new Vector2(curRoom.Position.x - curRoom.GetWidth, curRoom.Position.y - curRoom.GetHeight);

                    Vector2 curRoomSize = new Vector2(curRoomTopRight.x - curRoomBottomLeft.x, curRoomTopRight.y - curRoomBottomLeft.y);
                    Debug.Log("Room " + i + " size: " + curRoomSize);

                    float curRoomSpacingX = curRoomSize.x / pillarsX;
                    float curRoomSpacingY = curRoomSize.y / pillarsY;

                    for (int x = 1; x < pillarsX; x++)
                    {
                        pillarSpawner.transform.position = new Vector3(curRoomBottomLeft.x + (curRoomSpacingX * x), 2.3f, curRoomBottomLeft.y);
                        pillarSpawner.transform.rotation = new Quaternion(0, 90, 0, 0);
                        Instantiate(pillarPrefabNorth, pillarSpawner.transform.position, Quaternion.identity);

                        pillarSpawner.transform.position = new Vector3(curRoomBottomLeft.x +  (curRoomSpacingX * x), 2.3f, (curRoomBottomLeft.y + curRoomSize.y));
                        pillarSpawner.transform.rotation = new Quaternion(0, -90, 0, 0);
                        Instantiate(pillarPrefabNorth, pillarSpawner.transform.position, Quaternion.identity);
                    }
                    
                }
            }
        }
    }
}

