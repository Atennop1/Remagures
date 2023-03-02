using System;
using Remagures.Model.Flashing;
using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Model.Attacks
{
    public sealed class PhysicsTarget : MonoBehaviour, ITarget
    {
        public IHealth Health => _target.Health;
        public IFlashingable Flashingable => _target.Flashingable;

        private ITarget _target;

        public void Construct(ITarget target)
            => _target = target ?? throw new ArgumentNullException(nameof(target));
    }
}