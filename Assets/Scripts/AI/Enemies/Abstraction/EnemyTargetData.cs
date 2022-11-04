using UnityEngine;

namespace Remagures.AI.Enemies.Abstraction
{
    public sealed class EnemyTargetData : MonoBehaviour
    {
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float ChaseRadius { get; private set; }
        [field: SerializeField] public Transform Target { get; private set; }
    }
}