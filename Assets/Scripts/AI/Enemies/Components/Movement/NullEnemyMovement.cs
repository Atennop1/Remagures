using Remagures.AI.Enemies.Abstraction;
using UnityEngine;

namespace Remagures.AI.Enemies.Components.Movement
{
    public class NullEnemyMovement : MonoBehaviour, IEnemyMovement
    {
        public void Move(Transform targetTransform) { }
        public void StopMoving() { }
        public bool CanMove => true;
    }
}