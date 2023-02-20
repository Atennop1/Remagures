using System;
using Remagures.Model.Health;

namespace Remagures.Model.AI.Enemies
{
    public sealed class Enemy : IEnemy
    {
        public IEnemyMovement Movement { get; }
        public IHealth Health { get; }
        
        public EnemyAnimations Animations { get; }
        public StateMachine StateMachine { get; } = new();

        private Enemy(IEnemyMovement movement, IHealth health, EnemyAnimations animations)
        {
            Animations = animations ?? throw new ArgumentNullException(nameof(animations));
            Health = health ?? throw new ArgumentNullException(nameof(health));

            Movement = movement ?? throw new ArgumentNullException(nameof(movement));
            StateMachine.AddUniversalTransition(new DeadState(this), () => Health.CurrentValue < 0);
        }
    }
}
