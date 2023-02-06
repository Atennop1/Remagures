using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.HealthSystem;

namespace Remagures.Model.Character
{
    public class CharacterHealth : IHealth
    {
        public int MaxValue => _health.MaxValue;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private bool _isStunned;
        private readonly IHealth _health;

        public CharacterHealth(IHealth health)
            => _health = health ?? throw new ArgumentNullException(nameof(health));

        public async void TakeDamage(int amount)
        {
            if (_isStunned)
                return;

            _health.TakeDamage(amount);
            await StunCharacter();

            //if (!IsDead) 
            //    return;
            
            //_player.ChangeState(PlayerState.Dead);
            //_gameOver.SetGameOver();
        }

        public void Heal(int amount)
            => _health.Heal(amount);
        
        private async UniTask StunCharacter()
        {
            _isStunned = true;
            await UniTask.WaitForFixedUpdate();
            _isStunned = false;
        }
    }
}
