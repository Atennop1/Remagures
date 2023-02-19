using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemyMovement
    {
        bool CanMove { get; }
        void Move(Transform targetTransform);
        void StopMoving();
    }
}