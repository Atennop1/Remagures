using System;
using Remagures.Model.Flashing;
using Remagures.Model.Health;

namespace Remagures.Model.Damage
{
    public readonly struct Target
    {
        public readonly IHealth Health;
        public readonly IFlashingable Flashingable;

        public Target(IHealth health, IFlashingable flashingable)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Flashingable = flashingable ?? throw new ArgumentNullException(nameof(flashingable));
        }
    }
}