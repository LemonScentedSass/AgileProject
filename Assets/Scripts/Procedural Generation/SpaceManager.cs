using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathsHelper;
using UnityEngine.Tilemaps;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager instance;

    public Vector3 spaceSize;
    public LayerMask layerMask;
    public float maxDistance;
    public Tilemap Map;

    private SpaceStorage[,] _spaces;

    private void Awake()
    {
        if (SpaceManager.instance == null)
        {
            SpaceManager.instance = this;
        }
        else if (SpaceManager.instance != this)
        {
            Destroy(this);
        }
    }

    public void CalculateSpace()
    {
        if (maxDistance == 0 || Map == null)
        {
            Debug.Log("Please do not divide by 0");
            return;
        }

        Map.ResizeBounds();

        Debug.Log(Map.localBounds.min.x + "," + Map.localBounds.max.z);
        Debug.Log(Map.localBounds.min.y + "," + Map.localBounds.max.z);

        float xRange = GenericNumbers.Distance(Map.localBounds.min.x, Map.localBounds.max.x);
        float yRange = GenericNumbers.Distance(Map.localBounds.min.z, Map.localBounds.max.z);

        xRange = Mathf.CeilToInt(xRange / maxDistance);
        yRange = Mathf.CeilToInt(yRange / maxDistance);

        _spaces = new SpaceStorage[(int)xRange, (int)yRange];

        Vector3 startPot = new Vector3(Map.localBounds.min.x, 0f, Map.localBounds.min.z);

        for (int x = 0; x < _spaces.GetLength(0); x++)
        {
            for (int y = 0; y < _spaces.GetLength(1); y++)
            {
                Vector3 newPosition = startPot + new Vector3(x * maxDistance, 0f, y * maxDistance);
                GameObject go = new GameObject("x:" + x + "x y:" + y);
                go.transform.position = newPosition;
                _spaces[x, y] = go.AddComponent<SpaceStorage>();
            }
        }
    }
}
