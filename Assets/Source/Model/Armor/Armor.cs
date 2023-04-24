using System;

namespace Remagures.Model
{
    public sealed class Armor : IArmor
    {
        private readonly IArmorValue _armorValue;

        public Armor(IArmorValue armorValue)
            => _armorValue = armorValue ?? throw new ArgumentNullException(nameof(armorValue));

        public int AbsorbDamage(int damage)
        {
            var totalDamage = (int)(damage - _armorValue.Get() + 0.5f);

            if (totalDamage == 0)
                totalDamage = 1;

            return totalDamage;
        }
    }
}