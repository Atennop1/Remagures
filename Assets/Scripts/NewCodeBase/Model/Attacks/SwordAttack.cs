using System;
using Cysharp.Threading.Tasks;
using Remagures.View;

namespace Remagures.Model.Attacks
{
    public sealed class SwordAttack : IAttack
    {
        public IAttackData Data { get; }
        private readonly IAttackView _attackView;

        public SwordAttack(IAttackView attackView, IAttackData data)
        {
            _attackView = attackView ?? throw new ArgumentNullException(nameof(attackView));
            Data = data;
        }

        public async void Use()
        {
            _attackView.PlayAttackAnimation();
            await UniTask.Delay(Data.AttackingTimeInMilliseconds);
            _attackView.StopAttackAnimation();
        }
    }
}