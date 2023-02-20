using Remagures.Model.AI.Enemies;
using UnityEngine;

namespace Remagures.View.Enemies
{
    public class EnemyMovementView : MonoBehaviour, IEnemyMovementView
    {
        private EnemyAnimations _enemyAnimations;

        public void SetIsWakeUp(bool isActive)
            => _enemyAnimations.SetIsWakeUp(isActive);

        public void SetIsStaying(bool isActive)
            => _enemyAnimations.SetIsStaying(isActive);

        public void SetAnimationsVector(Vector2 vector)
            => _enemyAnimations.SetAnimationsVector(vector);
    }
}