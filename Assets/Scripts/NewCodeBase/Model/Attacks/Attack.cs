using Remagures.Model.Flashing;
using Remagures.Tools;

namespace Remagures.Model.Attacks
{
    public sealed class Attack : IAttack
    {
        public int Damage { get; }

        public Attack(int damage)
            => Damage = damage.ThrowExceptionIfLessOrEqualsZero();

        public void ApplyTo(ITarget target)
        {
            target.Health.TakeDamage(Damage);
            target.Flashingable.Flash(FlashColorType.Damage, FlashColorType.BeforeFlash);
        }
    }
}
