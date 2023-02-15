using Remagures.Tools;

namespace Remagures.Model.Magic
{
    public readonly struct MagicData
    {
        public readonly int ManaCost;
        public readonly int CooldownInMilliseconds;

        public MagicData(int manaCost, int cooldownInMilliseconds)
        {
            ManaCost = manaCost.ThrowExceptionIfLessOrEqualsZero();
            CooldownInMilliseconds = cooldownInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}