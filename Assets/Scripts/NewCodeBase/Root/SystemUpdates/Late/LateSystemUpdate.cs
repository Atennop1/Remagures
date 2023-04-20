using System;
using System.Collections.Generic;

namespace Remagures.Root
{
    public sealed class LateSystemUpdate : ILateSystemUpdate
    {
        private readonly List<ILateUpdatable> _updatables = new();

        public void UpdateAll() 
            => _updatables.ForEach(updatable => updatable.LateUpdate());
        
        public void Add(params ILateUpdatable[] updatables)
        {
            if (updatables == null)
                throw new ArgumentException("Can't add null updatable");

            _updatables.AddRange(updatables);
        }

        public void Remove(ILateUpdatable updatable)
        {
            if (updatable == null)
                throw new ArgumentException("Can't remove null updatable");

            if (!_updatables.Contains(updatable))
                throw new ArgumentException($"{updatable} updatable doesn't contains in system update");

            _updatables.Remove(updatable);
        }
    }
}