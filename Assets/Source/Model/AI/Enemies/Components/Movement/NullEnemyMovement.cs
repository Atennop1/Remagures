using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class NullEnemyMovement : IEnemyMovement
    {
        public bool CanMove => true;
        public Transform Transform => null;
        
        public void Move(Vector3 targetPosition) { }
        public void StopMoving() { }
    }
}