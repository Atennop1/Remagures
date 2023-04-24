using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class EnemyTargetData : IEnemyTargetData
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float ChaseRadius { get; private set; }
    }
}