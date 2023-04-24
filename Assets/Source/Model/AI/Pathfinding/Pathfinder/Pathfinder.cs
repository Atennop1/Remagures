using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class Pathfinder : IPathfinder
    {
        private readonly Grid _grid;
        private readonly DistanceCalculator _distanceCalculator = new();

        public Pathfinder(Grid grid)
            => _grid = grid ?? throw new ArgumentNullException(nameof(grid));

        public IReadOnlyList<IReadOnlyGridNode> FindPath(Vector3 startPosition, Vector3 targetPosition) //very scary algorithm :p
        {
            var startGridNode = _grid.GetNodeFromWorldPoint(startPosition);
            var targetGridNode = _grid.GetNodeFromWorldPoint(targetPosition);
            startGridNode.SetIsObstacle(true);

            var notUsedNodes = new List<GridNode>();
            var usedNodes = new HashSet<GridNode>();
            var nodesPathfindingDataDictionary = new Dictionary<IReadOnlyGridNode, GridNodePathfindingData>();
            notUsedNodes.Add(startGridNode);
        
            while (notUsedNodes.Count > 0)
            {
                var node = notUsedNodes[0];
                for (var i = 1; i < notUsedNodes.Count; i++)
                {
                    if (nodesPathfindingDataDictionary[notUsedNodes[i]].FCost > nodesPathfindingDataDictionary[node].FCost) 
                        continue;
                
                    if (nodesPathfindingDataDictionary[notUsedNodes[i]].HCost < nodesPathfindingDataDictionary[node].HCost)
                        node = notUsedNodes[i];
                }

                notUsedNodes.Remove(node);
                usedNodes.Add(node);

                if (node == targetGridNode)
                {
                    var pathBuilder = new PathBuilder(nodesPathfindingDataDictionary);
                    return pathBuilder.Build(startGridNode, targetGridNode);
                }

                foreach (var neighbour in _grid.GetNeighbors(node))
                {
                    if ((neighbour.IsObstacle && neighbour != targetGridNode) || usedNodes.Contains(neighbour))
                        continue;

                    var newCostToNeighbour = nodesPathfindingDataDictionary[node].GCost + _distanceCalculator.Calculate(node, neighbour);
                    if (newCostToNeighbour >= nodesPathfindingDataDictionary[neighbour].GCost && notUsedNodes.Contains(neighbour)) 
                        continue;
                
                    var gCost = newCostToNeighbour;
                    var hCost = _distanceCalculator.Calculate(neighbour, targetGridNode);

                    nodesPathfindingDataDictionary.Add(neighbour, new GridNodePathfindingData(gCost, hCost, node));

                    if (!notUsedNodes.Contains(neighbour))
                        notUsedNodes.Add(neighbour);
                }
            }

            return null;
        }
    }
}