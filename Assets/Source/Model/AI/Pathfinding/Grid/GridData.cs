using Remagures.Tools;

namespace Remagures.Model.AI.Pathfinding
{
    public readonly struct GridData
    {
        public readonly float NodeRadius;
        public readonly float NodeDiameter;

        public readonly int SizeX;
        public readonly int SizeY;

        public GridData(float nodeRadius, int sizeX, int sizeY)
        {
            NodeRadius = nodeRadius.ThrowExceptionIfLessThanZero();
            NodeDiameter = NodeRadius * 2;

            SizeX = sizeX.ThrowExceptionIfLessOrEqualsZero();
            SizeY = sizeY.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}