using Remagures.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.AI.Enemies
{
    public class EnemyWithTarget : SerializedMonoBehaviour, IEnemyWithTarget
    {
        [SerializeField] private IEnemy _enemy;
        [field: SerializeField] public EnemyTargetData TargetData { get; private set; }
        
        public IEnemyMovement Movement => _enemy.Movement;
        public Health Health => _enemy.Health;
        public EnemyAnimations Animations => _enemy.Animations;
        public StateMachine StateMachine => _enemy.StateMachine;
    }
}