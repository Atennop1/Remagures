using System;
using SaveSystem;
using UnityEngine;

namespace Remagures.Tools
{
    public sealed class TimeDifferenceCounter
    {
        private readonly ISaveStorage<DateTime> _storage;

        public TimeDifferenceCounter(ISaveStorage<DateTime> storage) 
            => _storage = storage ?? throw new ArgumentNullException(nameof(storage));

        public int GetTimeDifference()
        {
            var oldDate = _storage.Load();
            var difference = DateTime.Now.Subtract(oldDate);
            return (int)difference.TotalSeconds;
        }
    
        public void SaveCurrentTime()
            => _storage.Save(DateTime.Now);
    }
}
