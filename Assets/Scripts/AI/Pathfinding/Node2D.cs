using UnityEngine;

namespace Remagures.AI
{
    public class Node2D : IReadOnlyNode2D
    {
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;

        public int GridX { get;  }
        public int GridY { get;  }

        public bool Obstacle { get; private set; }
        public Vector3 WorldPosition { get; }
        public Node2D Parent { get; set; }

        public Node2D(bool obstacle, Vector3 worldPos, int gridX, int gridY)
        {
            Obstacle = obstacle;
            WorldPosition = worldPos;
        
            GridX = gridX;
            GridY = gridY;
        }

        public void SetObstacle(bool isOb)
        {
            Obstacle = isOb;
        }
    }

    public interface IReadOnlyNode2D
    {
        public int GridX { get; }
        public int GridY { get; }

        public bool Obstacle { get; }
        public Vector3 WorldPosition { get; }
    }
}