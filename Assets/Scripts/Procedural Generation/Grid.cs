using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public bool onlyDrawPathGizmos;
    public LayerMask unwalkableMask;
    public LayerMask obstacleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public TerrainType[] walkableRegions;

    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Awake()
    {
        if (Grid.instance == null)
            Grid.instance = this;
        if (Grid.instance != this)
            Destroy(this);
    }

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        //CreateGrid();
    }

    public int MaxSize { get { return gridSizeX * gridSizeY; } }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                bool hasObstacle = (Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask));

                int movementPenalty = 0;

                // raycast

                grid[x, y] = new Node(walkable, hasObstacle, worldPoint, x, y, movementPenalty);
            }
        }
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >=0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> path;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (onlyDrawPathGizmos)
        {
            if (path != null)
            {
                foreach (Node n in path)
                {
                    Debug.Log(n.hasObstacle);
                    Gizmos.color = Color.black;
                    Gizmos.color = (n.hasObstacle) ? Color.cyan : Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.01f));
                }
            }
        }
        else
        {
            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    
                    if (path != null)
                        if (path.Contains(n))
                        {
                            Gizmos.color = Color.black;
                            Gizmos.color = (n.hasObstacle) ? Color.black : Color.cyan;
                        }
                            

                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.01f));
                }
            }
        }        
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}
