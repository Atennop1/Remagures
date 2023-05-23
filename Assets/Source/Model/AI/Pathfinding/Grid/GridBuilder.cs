using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class GridBuilder
    {
        private readonly GridData _gridData;

        public GridBuilder(GridData gridData)
            => _gridData = gridData;

        public GridNode[,] Build(Vector3 startWorldCoordinates)
        {
            var nodes = new GridNode[_gridData.SizeX, _gridData.SizeY];

            for (var x = 0; x < _gridData.SizeX; x++)
            {
                for (var y = 0; y < _gridData.SizeY; y++)
                {
                    var nodePositionX = x * _gridData.NodeDiameter + _gridData.NodeRadius;
                    var nodePositionY = y * _gridData.NodeDiameter + _gridData.NodeRadius;
                    
                    var worldNodePosition = startWorldCoordinates + Vector3.right * nodePositionX + Vector3.up * nodePositionY;
                    nodes[x, y] = new GridNode(worldNodePosition, x, y);
                }
            }

            return nodes;
        }
    }
}