using System;

namespace Remagures.Model.Magic
{
    public sealed class Magic : IMagic
    {
        public IMagicData Data { get; }
        private readonly IMana _mana;

        public Magic(IMana mana, IMagicData data)
        {
            _mana = mana ?? throw new ArgumentNullException(nameof(mana));
            Data = data;
        }

        public bool CanActivate()
            => _mana.CurrentValue >= Data.ManaCost;

        public void Activate()
        {
            if (!CanActivate())
                throw new InvalidOperationException("Not enough mana");
            
            _mana.Decrease(Data.ManaCost);
        }
    }
}