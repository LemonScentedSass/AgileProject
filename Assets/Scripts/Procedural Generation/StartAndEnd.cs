using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapGeneration;
using UnityEngine.SceneManagement;

namespace LevelData
{
    public class StartAndEnd : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject startRoomObject;
        [SerializeField] private GameObject endRoomObject;

        public Image loadingScreen;

        public bool readyToSpawn;
        public GameObject groundPlane;

        public static StartAndEnd instance;

        private void Awake()
        {
            readyToSpawn = false; // Not ready to 'spawn' the player
            groundPlane.SetActive(false); // Because the player already exists though, turn off the ground so they can't walk bwahaha
            
            if (StartAndEnd.instance == null)
            {
                StartAndEnd.instance = this;
            }
            else if (StartAndEnd.instance != this)
            {
                Destroy(this);
            }
        }

        public void FindStartAndEnd(List<Room> rooms)
        {
            Room startRoom = rooms[0];

            Vector2 sRoomTopRight = new Vector2(startRoom.Position.x + startRoom.GetWidth, startRoom.Position.y + startRoom.GetHeight);
            Vector2 sRoomBottomLeft = new Vector2(startRoom.Position.x - startRoom.GetWidth, startRoom.Position.y - startRoom.GetHeight);

            Vector2 sRoomCenter = new Vector2((sRoomBottomLeft.x + sRoomTopRight.x) * .5f, (sRoomBottomLeft.y + sRoomTopRight.y) * .5f);
            if (startRoomObject != null)
            {
                startRoomObject.transform.position = new Vector3(sRoomCenter.x, 2.7f, sRoomCenter.y);
            }

            float minDistance = 0;

            for (int i = 1; i < rooms.Count; i++)
            {
                Room curRoom = rooms[i];

                if (curRoom.TurnedOff == false)
                {
                    Vector2 curRoomTopRight = new Vector2(curRoom.Position.x + curRoom.GetWidth, curRoom.Position.y + curRoom.GetHeight);
                    Vector2 curRoomBottomLeft = new Vector2(curRoom.Position.x - curRoom.GetWidth, curRoom.Position.y - curRoom.GetHeight);

                    Vector2 curRoomCenter = new Vector2((curRoomBottomLeft.x + curRoomTopRight.x) *.5f, (curRoomBottomLeft.y + curRoomTopRight.y) * .5f);

                    float newDistance = Vector2.Distance(curRoomCenter, sRoomCenter);
                    if (newDistance > minDistance)
                    {
                        //Debug.Log("End Room Found @ " + i);
                        Vector2 eRoomCenter = curRoomCenter;
                        //Debug.Log("end Room Center: " + eRoomCenter);
                        if (endRoomObject != null)
                        {
                            endRoomObject.transform.position = new Vector3(eRoomCenter.x, 2.7f, eRoomCenter.y);
                        }
                        minDistance = newDistance;
                    }
                }

                if (i == rooms.Count - 1)
                {
                    readyToSpawn = true;
                }
            }
        }

        private void Update()
        {
            if (readyToSpawn)
            {
                InitSpawn();
                readyToSpawn = false;
            }
        }

        public void InitSpawn()
        {
            playerPrefab.transform.position = startRoomObject.transform.position;
            groundPlane.SetActive(true);

            loadingScreen.gameObject.SetActive(false);
        }
    }
}