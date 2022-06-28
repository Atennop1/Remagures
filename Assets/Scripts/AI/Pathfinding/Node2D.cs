using UnityEngine;

public class Node2D : IReadOnlyNode2D
{
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; } }

    public int GridX { get; private set; }
    public int GridY { get; private set; }

    public bool Obstacle { get; private set; }
    public Vector3 WorldPosition { get; private set; }
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