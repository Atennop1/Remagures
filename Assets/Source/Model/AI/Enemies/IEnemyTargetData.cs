using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemyTargetData
    {
        Transform Transform { get; }
        float AttackRadius { get; }
        float ChaseRadius { get; }
    }
}