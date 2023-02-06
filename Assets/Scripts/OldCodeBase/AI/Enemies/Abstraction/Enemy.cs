using Remagures.Components;
using Remagures.Model.HealthSystem;
using UnityEngine;
using SM = Remagures.AI.StateMachine.StateMachine;

namespace Remagures.AI.Enemies
{
    [RequireComponent(typeof(EnemyAnimations))]
    [RequireComponent(typeof(IEnemyMovement))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public IEnemyMovement Movement { get; private set;  }
        public Health Health { get; private set;  }
        
        public EnemyAnimations Animations { get; private set; }
        public SM StateMachine { get; private set; }

        private void Awake()
        {
            Animations = GetComponent<EnemyAnimations>();
            Health = GetComponent<Health>();
            
            Movement = GetComponent<IEnemyMovement>();
            StateMachine = new SM();
            
            StateMachine.AddAnyTransition(new DeadState(this), () => Health.Value < 0);
        }
    }
}
