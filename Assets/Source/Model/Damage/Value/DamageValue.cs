using Remagures.Tools;

namespace Remagures.Model.Damage
{
    public sealed class DamageValue : IDamageValue
    {
        private readonly int _value;

        public DamageValue(int value) 
            => _value = value.ThrowExceptionIfLessOrEqualsZero();

        public int Get()
            => _value;
    }
}