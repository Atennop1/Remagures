using Remagures.Model.Flashing;
using Remagures.Tools;

namespace Remagures.Model.Damage
{
    public sealed class Damage : IDamage
    {
        public int Value { get; }

        public Damage(int damage)
            => Value = damage.ThrowExceptionIfLessOrEqualsZero();

        public void ApplyTo(ITarget target)
        {
            target.Health.TakeDamage(Value);
            target.Flashingable.Flash(FlashColorType.Damage, FlashColorType.BeforeFlash);
        }
    }
}
