using Remagures.Tools;

namespace Remagures.Model.Health
{
    public sealed class Armor : IArmor
    {
        private float _armorAmount;

        public Armor(float armor)
            => _armorAmount = armor.ThrowExceptionIfLessOrEqualsZero();

        public void SetArmor(float armor)
            => _armorAmount = armor.ThrowExceptionIfLessOrEqualsZero();

        public int AbsorbDamage(int damage)
        {
            var totalDamage = (int)(damage - _armorAmount + 0.5f);

            if (totalDamage == 0)
                totalDamage = 1;

            return totalDamage;
        }
    }
}