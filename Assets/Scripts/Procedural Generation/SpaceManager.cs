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

    public SpaceStorage[,] _spaces;

    public InputHandler input;

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

        input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        if (input.testKey)
        {
            CalculateSpace();
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

        Debug.Log(Map.localBounds.min.x + "," + Map.localBounds.min.z);
        Debug.Log(Map.localBounds.max.x + "," + Map.localBounds.max.z);

        float xRange = GenericNumbers.Distance(Map.localBounds.min.x, Map.localBounds.max.x);
        float yRange = GenericNumbers.Distance(Map.localBounds.min.z, Map.localBounds.max.z);

        xRange = Mathf.CeilToInt(xRange / maxDistance);
        yRange = Mathf.CeilToInt(yRange / maxDistance);

        _spaces = new SpaceStorage[(int)xRange, (int)yRange];

        Vector3 startPos = new Vector3(Map.localBounds.min.x, 0f, Map.localBounds.min.z);

        for (int x = 0; x < _spaces.GetLength(0); x++)
        {
            for (int y = 0; y < _spaces.GetLength(1); y++)
            {
                Vector3 newPosition = startPos + new Vector3(x * maxDistance, 0f, y * maxDistance);
                GameObject go = new GameObject("x:" + x + "x y:" + y);
                //Debug.Log(go.transform.position);
                //go.SetActive(false);
                go.transform.position = newPosition;
                //Debug.Log(go.transform.position);
                _spaces[x, y] = go.AddComponent<SpaceStorage>();
                go.GetComponent<SpaceStorage>().FillTransforms();
                //Collider[] hits = Physics.OverlapBox(newPosition, spaceSize);
                //Collider[] hits = Physics.OverlapBox(newPosition, spaceSize, Quaternion.identity, layerMask);
                //Debug.Log(hits.Length);
            }
        }
    }
}
