using Remagures.Tools;

namespace Remagures.Model.Magic
{
    public readonly struct MagicData : IMagicData
    {
        public int ManaCost { get; }
        public int CooldownInMilliseconds { get; }
        public int ApplyingTimeInMilliseconds { get; }

        public MagicData(int manaCost, int cooldownInMilliseconds, int applyingTimeInMilliseconds)
        {
            ManaCost = manaCost.ThrowExceptionIfLessOrEqualsZero();
            CooldownInMilliseconds = cooldownInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
            ApplyingTimeInMilliseconds = applyingTimeInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}