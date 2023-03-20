using System;
using UnityEngine;

namespace Remagures.Tools
{
    public sealed class TimeDifferenceCounter
    {
        private readonly IStorage _storage;
        private readonly string _key;

        public TimeDifferenceCounter(IStorage storage, string key)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public int GetTimeDifference()
        {
            var oldDate = _storage.Load<DateTime>(_key);
            var difference = DateTime.Now.Subtract(oldDate);
            return (int)difference.TotalSeconds;
        }
    
        public void SaveCurrentTime()
            => _storage.Save(DateTime.Now, _key);
    }
}
