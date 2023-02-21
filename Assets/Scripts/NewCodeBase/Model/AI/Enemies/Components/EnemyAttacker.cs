using System.Threading;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.AI.Enemies
{
    public class EnemyAttacker
    {
        public bool IsAttacking { get; private set; }

        private readonly IAttackingEnemyAnimations _enemyAnimations;
        private CancellationTokenSource _cancellationTokenSource;
        
        public async void Attack()
        {
            if (IsAttacking)
                _cancellationTokenSource.Cancel();
            
            _cancellationTokenSource = new CancellationTokenSource();
            await AttackTask(_cancellationTokenSource.Token);
        }

        private async UniTask AttackTask(CancellationToken cancellationToken)
        {
            IsAttacking = true;
            _enemyAnimations.SetIsAttacking(false);
            _enemyAnimations.SetIsAttacking(true);

            await UniTask.Delay(350);
            
            if (cancellationToken.IsCancellationRequested)
                return;

            IsAttacking = false;
            _enemyAnimations.SetIsStaying(true);
            _enemyAnimations.SetIsAttacking(false);
        }
    }
}