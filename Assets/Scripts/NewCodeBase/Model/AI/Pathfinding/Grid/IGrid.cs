using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public interface IGrid
    {
        GridNode[,] Nodes { get;}
        IReadOnlyList<GridNode> GetNeighbors(GridNode gridNode);
        GridNode GetNodeFromWorldPoint(Vector3 worldPosition);
    }
}