using UnityEngine;

namespace Remagures.View.Enemies
{
    public interface IEnemyMovementView
    {
        void SetIsStaying(bool isActive);
        void SetAnimationsVector(Vector2 vector);
    }
}