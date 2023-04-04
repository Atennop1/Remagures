using System;
using Remagures.Model.Flashing;
using Remagures.Model.Health;

namespace Remagures.Model.Damage
{
    public readonly struct Target : ITarget
    {
        public IHealth Health { get; }
        public IFlashingable Flashingable { get; }

        public Target(IHealth health, IFlashingable flashingable)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Flashingable = flashingable ?? throw new ArgumentNullException(nameof(flashingable));
        }
    }
}