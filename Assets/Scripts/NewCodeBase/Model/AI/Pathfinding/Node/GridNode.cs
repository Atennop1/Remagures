using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class GridNode : IReadOnlyGridNode
    {
        public int PositionX { get; }
        public int PositionY { get; }

        public bool IsObstacle { get; private set; }
        public Vector3 WorldPosition { get; }

        public GridNode(Vector3 worldPosition, int gridPositionX, int gridPositionY)
        {
            WorldPosition = worldPosition;
            PositionX = gridPositionX.ThrowExceptionIfLessThanZero();
            PositionY = gridPositionY.ThrowExceptionIfLessThanZero();
        }

        public void SetIsObstacle(bool isObstacle)
            => IsObstacle = isObstacle;
    }
}