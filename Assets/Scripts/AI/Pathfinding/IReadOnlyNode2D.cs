using UnityEngine;

namespace Remagures.AI.Pathfinding
{
    public interface IReadOnlyNode2D
    {
        public int GridX { get; }
        public int GridY { get; }

        public bool Obstacle { get; }
        public Vector3 WorldPosition { get; }
    }
}