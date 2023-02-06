using UnityEngine;

namespace Remagures.AI.Enemies
{
    public interface IEnemyMovement
    {
        void Move(Transform targetTransform);
        void StopMoving();
        bool CanMove { get; }
    }
}