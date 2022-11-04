using UnityEngine;

namespace Remagures.AI.Enemies.Abstraction
{
    public interface IEnemyMovement
    {
        void Move(Transform targetTransform);
        void StopMoving();
        bool CanMove { get; }
    }
}