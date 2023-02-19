using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public interface IPathfinder
    {
        IReadOnlyList<IReadOnlyGridNode> FindPath(Vector3 startPosition, Vector3 targetPosition);
    }
}