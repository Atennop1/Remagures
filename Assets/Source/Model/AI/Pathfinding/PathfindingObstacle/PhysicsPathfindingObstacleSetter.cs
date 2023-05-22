using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class PhysicsPathfindingObstacleSetter : SerializedMonoBehaviour
    {
        [SerializeField] private bool _isSettingToRight;
        [SerializeField] private bool _isSettingToDown;

        [Space]
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _offset;

        private PathfindingObstacleSetter _pathfindingObstacleSetter;

        private void Awake()
        {
            var pathfindingObstacleSetterData = new PathfindingObstacleSetterData(_isSettingToRight, _isSettingToDown, _size, _offset);
            _pathfindingObstacleSetter = new PathfindingObstacleSetter(transform, pathfindingObstacleSetterData);
        }

        public void Activate(Grid grid)
            => _pathfindingObstacleSetter.Activate(grid);

        public void Deactivate()
            => _pathfindingObstacleSetter.Deactivate();
    }
}