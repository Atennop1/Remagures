using UnityEngine;

namespace Remagures.AI.Enemies
{
    public class NullEnemyMovement : MonoBehaviour, IEnemyMovement
    {
        public void Move(Transform targetTransform) { }
        public void StopMoving() { }
        public bool CanMove => true;
    }
}