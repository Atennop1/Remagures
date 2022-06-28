using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    [field: SerializeField] public bool Debug { get; private set; }
    [field: SerializeField] public Vector3 GridWorldSize { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public float NodeRadius { get; private set; }
    [field: SerializeField] public Tilemap Obstaclemap { get; private set; }

    public Node2D[,] Grid { get; private set; }
    public List<Node2D> pathForGizmos = new List<Node2D>();

    private Vector3 _worldBottomLeft;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;

    private void Awake()
    {
        _nodeDiameter = NodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        Grid = new Node2D[_gridSizeX, _gridSizeY];
        _worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2 + Offset;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = _worldBottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) + Vector3.up * (y * _nodeDiameter + NodeRadius);
                Grid[x, y] = new Node2D(false, worldPoint, x, y);

                if (Obstaclemap.HasTile(Obstaclemap.WorldToCell(Grid[x, y].WorldPosition)))
                    Grid[x, y].SetObstacle(true);
                else
                    Grid[x, y].SetObstacle(false);
            }
        }
    }

    public List<Node2D> GetNeighbors(Node2D node)
    {
        List<Node2D> neighbors = new List<Node2D>();

        if (node.GridX >= 0 && node.GridX < _gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < _gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY + 1]);

        if (node.GridX >= 0 && node.GridX < _gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < _gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY - 1]);

        if (node.GridX + 1 >= 0 && node.GridX + 1 < _gridSizeX && node.GridY >= 0 && node.GridY < _gridSizeY)
            neighbors.Add(Grid[node.GridX + 1, node.GridY]);

        if (node.GridX - 1 >= 0 && node.GridX - 1 < _gridSizeX && node.GridY >= 0 && node.GridY < _gridSizeY)
            neighbors.Add(Grid[node.GridX - 1, node.GridY]);

        return neighbors;
    }

    public Node2D NodeFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x - 0.5f + (_gridSizeX / 2) - Offset.x);
        int y = Mathf.RoundToInt(worldPosition.y + (_gridSizeY / 2) - Offset.y) - 1;
        return Grid[x, y];
    }

    private void OnDrawGizmos()
    {
        if (Debug == true)
        {
            Gizmos.DrawWireCube(transform.position + Offset, new Vector3(GridWorldSize.x, GridWorldSize.y, 1));

            if (Grid != null)
            {
                foreach (Node2D n in Grid)
                {
                    if (n.Obstacle)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.white;

                    if (pathForGizmos != null && pathForGizmos.Contains(n))
                        Gizmos.color = Color.black;
                        
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeRadius - 0.2f));
                }
            }
        }
        
    }
}