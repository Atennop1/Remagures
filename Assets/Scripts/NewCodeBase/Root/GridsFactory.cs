using Remagures.Model.AI.Pathfinding;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Grid = Remagures.Model.AI.Pathfinding.Grid;

namespace Remagures.Root
{
    public sealed class GridsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private bool _isDebugActive;
        
        [SerializeField] private float _nodeRadius;
        [SerializeField] private int _gridSizeX;
        [SerializeField] private int _gridSizeY;
        
        [Space]
        [SerializeField] private Vector3 _gridWorldSize;
        [SerializeField] private Vector3 _gridOffset;
        [SerializeField] private Tilemap[] _obstaclesTilemaps;

        private GridNode[,] _nodes;
        
        public IGrid Create()
        {
            var nodeDiameter = _nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / nodeDiameter);

            var gridData = new GridData(_nodeRadius, _gridSizeX, _gridSizeY);
            var startWorldCoordinates = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.up * _gridWorldSize.y / 2 + _gridOffset;
            
            var gridBuilder = new GridBuilder(gridData);
            _nodes = gridBuilder.Build(startWorldCoordinates);

            var gridObstaclesApplier = new GridObstaclesApplier(_obstaclesTilemaps, gridData);
            gridObstaclesApplier.Apply(_nodes);

            var grid = new Grid(_nodes, gridData, _gridOffset);
            var obstacleSetters = FindObjectsOfType<PhysicsPathfindingObstacleSetter>();
            
            obstacleSetters.ForEach(setter => setter.Activate(grid));
            return grid;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(_gridOffset, _gridWorldSize);
            
            if (_isDebugActive != true || _nodes == null) 
                return;
        
            foreach (var node in _nodes)
            {
                Gizmos.color = node.IsObstacle ? Color.red : Color.white;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.3f);
            }
        }
    }
}