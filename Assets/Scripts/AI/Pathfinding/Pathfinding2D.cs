using UnityEngine;
using System.Collections.Generic;

public class Pathfinding2D : MonoBehaviour
{
    private List<Node2D> _path = new List<Node2D>();
    public IReadOnlyList<IReadOnlyNode2D> Path => _path;

    [field: SerializeField] public Grid2D Grid { get; private set; }

    private Node2D _seekerNode;
    private Node2D _targetNode;
    private Node2D _pastNode;

    private void Start()
    {   
        _pastNode = Grid.Grid[Grid.NodeFromWorldPoint(transform.position).GridX, Grid.NodeFromWorldPoint(transform.position).GridY];
        _pastNode.SetObstacle(true);
    }

    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        _pastNode.SetObstacle(false);
        _pastNode = Grid.Grid[Grid.NodeFromWorldPoint(transform.position).GridX, Grid.NodeFromWorldPoint(transform.position).GridY];
        _pastNode.SetObstacle(true);
        
        _seekerNode = Grid.NodeFromWorldPoint(startPos);
        _targetNode = Grid.NodeFromWorldPoint(targetPos);

        List<Node2D> openSet = new List<Node2D>();
        HashSet<Node2D> closedSet = new HashSet<Node2D>();
        openSet.Add(_seekerNode);
        
        while (openSet.Count > 0)
        {
            Node2D node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost <= node.FCost)
                {
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == _targetNode)
            {
                RetracePath(_seekerNode, _targetNode);
                return;
            }

            foreach (Node2D neighbour in Grid.GetNeighbors(node))
            {
                if ((neighbour.Obstacle && neighbour != _targetNode) || closedSet.Contains(neighbour))
                    continue;

                int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, _targetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    private void RetracePath(Node2D startNode, Node2D endNode)
    {
        Node2D currentNode = endNode;
        while (currentNode != startNode)
        {
            _path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        _path.Reverse();
        _path.Remove(startNode);
    }

    private int GetDistance(Node2D nodeA, Node2D nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;             
        foreach (Node2D node in Path)
            Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.3f);
    }
}