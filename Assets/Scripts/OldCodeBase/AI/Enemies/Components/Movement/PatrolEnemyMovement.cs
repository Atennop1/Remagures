using Remagures.AI.Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.AI.Enemies
{
    public class PatrolEnemyMovement : SerializedMonoBehaviour, IEnemyMovement
    {
        [SerializeField] private Pathfinding2D _pathfinding;
        [SerializeField] private IEnemyMovement _enemyMovement;
        [FormerlySerializedAs("Path")] [SerializeField] private Transform[] _path;
        
        public Transform CurrentPointTransform => _path[_currentPoint];
        private int _currentPoint;
        
        public void Move(Transform targetTransform)
        {
            _enemyMovement.Move(targetTransform);
            
            if (targetTransform == CurrentPointTransform && _pathfinding.Path.Count is 0 or 0)
                ChangeGoal();
        }

        public void StopMoving()
            => _enemyMovement.StopMoving();
        
        public bool CanMove 
            => _enemyMovement.CanMove;

        private void ChangeGoal()
        {
            if (_currentPoint == _path.Length - 1) _currentPoint = 0;
            else _currentPoint++;
        }
    }
}