using System.Collections.Generic;
using UnityEngine;

namespace Remagures.AI
{
    public class Pathfinding2D : MonoBehaviour
    {
        public IReadOnlyList<IReadOnlyNode2D> Path => _path;
        private readonly List<Node2D> _path = new();

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

            var openSet = new List<Node2D>();
            var closedSet = new HashSet<Node2D>();
            openSet.Add(_seekerNode);
        
            while (openSet.Count > 0)
            {
                var node = openSet[0];
                for (var i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost > node.FCost) continue;
                
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == _targetNode)
                {
                    RetracePath(_seekerNode, _targetNode);
                    return;
                }

                foreach (var neighbour in Grid.GetNeighbors(node))
                {
                    if ((neighbour.Obstacle && neighbour != _targetNode) || closedSet.Contains(neighbour))
                        continue;

                    var newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour)) continue;
                
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, _targetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        private void RetracePath(Node2D startNode, Node2D endNode)
        {
            _path.Clear();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                _path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            _path.Reverse();
            _path.Remove(startNode);
        }

        private int GetDistance(IReadOnlyNode2D nodeA, IReadOnlyNode2D nodeB)
        {
            var distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            var distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (distanceX > distanceY)
                return 14 * distanceY + 10 * (distanceX - distanceY);
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;             
            foreach (var node in _path)
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * 0.3f);

            if (_targetNode == null) return;
        
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(_targetNode.WorldPosition, Vector3.one * 0.3f);
        }
    }
}