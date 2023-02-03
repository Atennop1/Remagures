using System;
using System.Collections.Generic;

namespace Remagures.Root
{
    public sealed class SystemUpdate : ISystemUpdate
    {
        private readonly List<IUpdatable> _updatables;

        public SystemUpdate() => _updatables = new List<IUpdatable>();

        public void Add(params IUpdatable[] updatables)
        {
            if (updatables == null)
                throw new ArgumentException("Can't add null updatable");

            _updatables.AddRange(updatables);
        }

        public void Remove(IUpdatable updatable)
        {
            if (updatable == null)
                throw new ArgumentException("Can't remove null updatable");

            if (!_updatables.Contains(updatable))
                throw new ArgumentException($"{updatable} updatable doesn't contains in system update");

            _updatables.Remove(updatable);
        }

        public void UpdateAll() => _updatables.ForEach(updatable => updatable.Update());
    }
}