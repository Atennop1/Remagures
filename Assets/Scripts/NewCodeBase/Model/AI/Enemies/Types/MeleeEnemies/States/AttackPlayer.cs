using System;
using System.Threading.Tasks;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class AttackPlayer : IState
    {
        public bool IsAttacking { get; private set; }
        
        private readonly MeleeEnemy _meleeEnemy;

        public AttackPlayer(MeleeEnemy meleeEnemy)
            => _meleeEnemy = meleeEnemy ?? throw new ArgumentNullException(nameof(meleeEnemy));

        public async void OnEnter()
        {
            _meleeEnemy.Movement.StopMoving();
            _meleeEnemy.StartAttackCoroutine();
            await AttackTask();
        }

        private async Task AttackTask()
        {
            IsAttacking = true;
            await Task.Delay(1);
            IsAttacking = false;
        }
        
        public void OnExit() { }
        public void Update() { }
    }
}