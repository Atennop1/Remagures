using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemyAnimations //TODO i think it can be renamed to IEnemyView with some refactoring
    {
        void SetIsWakeUp(bool isActive);
        void SetIsStaying(bool isActive);
        void SetIsDead(bool isActive);
        void SetIsKnocked(bool isActive);
        void SetAnimationsVector(Vector2 vector);
    }
}