using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public class Grid : IGrid
    {
        public GridNode[,] Nodes { get; }
        
        private readonly GridData _gridData;
        private readonly Vector3 _worldOffset;

        public Grid( GridNode[,] nodes, GridData gridData, Vector3 worldOffset)
        {
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            _gridData = gridData;
            _worldOffset = worldOffset;
        }

        public IReadOnlyList<GridNode> GetNeighbors(GridNode gridNode)
        {
            var neighbors = new List<GridNode>();

            for (var y = 1; y > -2; y--)
            {
                for (var x = -1; x < 2; x++)
                {
                    if (x == -1 && y == 1 || x == -1 && y == -1 || x == 1 && y == 1 || x == 1 && y == -1)
                        continue;
                    
                    try { var node = Nodes[gridNode.PositionX + x, gridNode.PositionY + y]; }
                    catch { continue; }
                    
                    neighbors.Add(Nodes[gridNode.PositionX + x, gridNode.PositionY + y]);
                }
            }

            return neighbors;
        }

        public GridNode GetNodeFromWorldPoint(Vector3 worldPosition)
        {
            var x = Mathf.RoundToInt(worldPosition.x - 0.5f + _gridData.SizeX / 2 - _worldOffset.x);
            var y = Mathf.RoundToInt(worldPosition.y + _gridData.SizeY / 2 - _worldOffset.y) - 1;
            return Nodes[x, y];
        }
    }
}