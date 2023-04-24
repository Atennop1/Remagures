using Remagures.Tools;

namespace Remagures.Model.Attacks
{
    public struct AttackData : IAttackData
    {
        public int UsingCooldownInMilliseconds { get; }
        public int AttackingTimeInMilliseconds { get; }

        public AttackData(int usingCooldownInMilliseconds, int attackingTimeInMilliseconds)
        {
            UsingCooldownInMilliseconds = usingCooldownInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
            AttackingTimeInMilliseconds = attackingTimeInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}