using System.Threading.Tasks;
using Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.MeleeEnemies
{
    public sealed class AttackPlayer : IState
    {
        public bool IsAttacking { get; private set; }
        private readonly MeleeEnemy _meleeEnemy;

        public AttackPlayer(MeleeEnemy meleeEnemy)
        {
            _meleeEnemy = meleeEnemy;
        }

        public async void OnEnter()
        {
            _meleeEnemy.Movement.StopMoving();
            _meleeEnemy.StartAttackCoroutine();
            await Attack();
        }

        public void OnExit() { }
        public void Tick() { }

        private async Task Attack()
        {
            IsAttacking = true;
            await Task.Delay(1);
            IsAttacking = false;
        }
    }
}