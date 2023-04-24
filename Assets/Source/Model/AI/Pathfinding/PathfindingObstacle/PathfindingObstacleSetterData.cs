using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public readonly struct PathfindingObstacleSetterData
    {
        public readonly bool IsSettingToRight;
        public readonly bool IsSettingToDown;
        
        public readonly Vector2 Size;
        public readonly Vector3 Offset;

        public PathfindingObstacleSetterData(bool isSettingToRight, bool isSettingToDown, Vector2 size, Vector2 offset)
        {
            IsSettingToRight = isSettingToRight;
            IsSettingToDown = isSettingToDown;
            
            Size = size;
            Offset = offset;
        }
    }
}