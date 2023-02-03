using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Remagures.AI
{
    public class Grid2D : MonoBehaviour
    {
        [FormerlySerializedAs("ActiveDebug")] [SerializeField] private bool _activeDebug;
        [FormerlySerializedAs("GridWorldSize")] [SerializeField] private Vector3 _gridWorldSize;
        [FormerlySerializedAs("Offset")] [SerializeField] private Vector3 _offset;

        [FormerlySerializedAs("NodeRadius")] [SerializeField] private float _nodeRadius;
        [FormerlySerializedAs("ObstacleMaps")] [SerializeField] private Tilemap[] _obstacleMaps;

        public Node2D[,] Grid { get; private set; }

        private Vector3 _worldBottomLeft;
        private float _nodeDiameter;
    
        private int _gridSizeX;
        private int _gridSizeY;

        private void Awake()
        {
            _nodeDiameter = _nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
            CreateGrid();
        }
    
        private void CreateGrid()
        {
            Grid = new Node2D[_gridSizeX, _gridSizeY];
            _worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.up * _gridWorldSize.y / 2 + _offset;

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    var worldPoint = _worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.up * (y * _nodeDiameter + _nodeRadius);
                    Grid[x, y] = new Node2D(false, worldPoint, x, y);

                    foreach (var map in _obstacleMaps)
                    {
                        if (map.HasTile(map.WorldToCell(Grid[x, y].WorldPosition)))
                        {
                            Grid[x, y].SetObstacle(true);
                            break;
                        }
                        Grid[x, y].SetObstacle(false);
                    }
                }
            }
        }

        public List<Node2D> GetNeighbors(Node2D node)
        {
            var neighbors = new List<Node2D>();

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
            var x = Mathf.RoundToInt(worldPosition.x - 0.5f + (_gridSizeX / 2) - _offset.x);
            var y = Mathf.RoundToInt(worldPosition.y + (_gridSizeY / 2) - _offset.y) - 1;
            return Grid[x, y];
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(_offset, _gridWorldSize);
            
            if (_activeDebug != true || Grid == null) return;
        
            foreach (var node in Grid)
            {
                Gizmos.color = node.Obstacle ? Color.red : Color.white;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeRadius - 0.2f));
            }
        }
    }
}