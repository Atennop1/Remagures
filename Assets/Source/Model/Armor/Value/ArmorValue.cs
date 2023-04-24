using Remagures.Tools;

namespace Remagures.Model
{
    public sealed class ArmorValue : IArmorValue
    {
        private float _value;

        public ArmorValue(float value) 
            => _value = value.ThrowExceptionIfLessOrEqualsZero();

        public void Set(float value)
            => _value = value.ThrowExceptionIfLessOrEqualsZero();
        
        public float Get()
            => _value;
    }
}