using System;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.Magic
{
    public class MagicApplier //TODO give this responsibility to CharacterMagicApplier and delete argument from Apply method 
    {
        private readonly IMana _mana;
        private bool _canApply;

        public MagicApplier(IMana mana)
            => _mana = mana ?? throw new ArgumentNullException(nameof(mana));

        public void Apply(IMagic magic)
        {
            if (_mana.CurrentValue < magic.Data.ManaCost || _canApply)
                return;
            
            magic.Activate();
            _mana.Decrease(magic.Data.ManaCost);
            ApplyCooldown(magic.Data.CooldownInMilliseconds);
        }

        private async void ApplyCooldown(int cooldownInMilliseconds)
        {
            _canApply = false;
            var timer = 0;

            while (timer < cooldownInMilliseconds)
            {
                await UniTask.Yield();
                timer += (int)(UnityEngine.Time.deltaTime * 1000);
            }

            _canApply = true;
        }
    }
}