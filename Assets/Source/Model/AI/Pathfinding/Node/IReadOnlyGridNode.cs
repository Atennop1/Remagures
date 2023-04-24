using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public interface IReadOnlyGridNode
    {
        int PositionX { get; }
        int PositionY { get; }

        bool IsObstacle { get; }
        Vector3 WorldPosition { get; }
    }
}