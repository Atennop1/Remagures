using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class PathfindingObstacleSetter
    {
        private readonly Transform _transform;
        private readonly PathfindingObstacleSetterData _data;
        
        private readonly List<GridNode> _obstacleNodes = new();

        public PathfindingObstacleSetter(Transform transform, PathfindingObstacleSetterData data)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _data = data;
        }

        public void Activate(Grid grid)
        {
            if (grid == null)
                return;

            for (var x = (int)-(_data.Size.x / 2); x < _data.Size.x / 2; x++)
            {
                for (var y = (int)-(_data.Size.y / 2); y < _data.Size.y / 2; y++)
                {
                    var node = grid.GetNodeFromWorldPoint(_transform.position + _data.Offset);
                    
                    var xId = node.PositionX + (_data.IsSettingToRight ? -x : x);
                    var yId = node.PositionY + (_data.IsSettingToDown ? y : -y);

                    _obstacleNodes.Add(grid.Nodes[xId, yId]);
                    grid.Nodes[xId, yId].SetIsObstacle(true);
                }
            }
        }

        public void Deactivate()
        {
            foreach (var node in _obstacleNodes)
                node.SetIsObstacle(false);
        }
    }
}

