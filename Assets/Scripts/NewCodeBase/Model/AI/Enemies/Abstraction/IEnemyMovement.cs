using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemyMovement
    {
        bool CanMove { get; }
        Transform Transform { get; }
        
        void Move(Vector3 targetPosition);
        void StopMoving();
    }
}