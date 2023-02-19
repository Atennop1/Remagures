using System;
using System.Collections.Generic;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class PathBuilder
    {
        private readonly Dictionary<IReadOnlyGridNode, GridNodePathfindingData> _nodesPathfindingDataDictionary;

        public PathBuilder(Dictionary<IReadOnlyGridNode, GridNodePathfindingData> nodesPathfindingDataDictionary)
            => _nodesPathfindingDataDictionary = nodesPathfindingDataDictionary ?? throw new ArgumentException(nameof(nodesPathfindingDataDictionary));

        public IReadOnlyList<IReadOnlyGridNode> Build(IReadOnlyGridNode startGridNode, IReadOnlyGridNode endGridNode)
        {
            var path = new List<IReadOnlyGridNode>();
            var currentNode = endGridNode;

            while (currentNode != startGridNode)
            {
                path.Add(currentNode);
                currentNode = _nodesPathfindingDataDictionary[currentNode].Parent;
            }

            path.Reverse();
            path.Remove(startGridNode);
            return path;
        }
    }
}