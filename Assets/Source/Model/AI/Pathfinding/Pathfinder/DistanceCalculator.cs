using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class DistanceCalculator
    {
        public int Calculate(IReadOnlyGridNode firstGridNode, IReadOnlyGridNode secondGridNode)
        {
            var distanceX = Mathf.Abs(firstGridNode.PositionX - secondGridNode.PositionX);
            var distanceY = Mathf.Abs(firstGridNode.PositionY - secondGridNode.PositionY);

            if (distanceX > distanceY)
                return 14 * distanceY + 10 * (distanceX - distanceY);
            
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}