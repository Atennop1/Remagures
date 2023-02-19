using System;
using Remagures.Tools;

namespace Remagures.Model.AI.Pathfinding
{
    public readonly struct GridNodePathfindingData
    {
        public readonly int GCost;
        public readonly int HCost;
        public int FCost => GCost + HCost;
        
        public readonly GridNode Parent;

        public GridNodePathfindingData(int gCost, int hCost, GridNode parent)
        {
            GCost = gCost.ThrowExceptionIfLessThanZero();
            HCost = hCost.ThrowExceptionIfLessThanZero();
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }
    }
}