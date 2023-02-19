using UnityEngine;
using SM = Remagures.Model.AI.StateMachine.StateMachine;

namespace Remagures.Model.AI.Enemies
{
    [RequireComponent(typeof(EnemyAnimations))]
    [RequireComponent(typeof(IEnemyMovement))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public IEnemyMovement Movement { get; private set;  }
        public Health.Health Health { get; private set;  }
        
        public EnemyAnimations Animations { get; private set; }
        public SM StateMachine { get; private set; }

        private void Awake()
        {
            Animations = GetComponent<EnemyAnimations>();
            Health = GetComponent<Health.Health>();
            
            Movement = GetComponent<IEnemyMovement>();
            StateMachine = new SM();
            
            StateMachine.AddUniversalTransition(new DeadState(this), () => Health.Value < 0);
        }
    }
}
