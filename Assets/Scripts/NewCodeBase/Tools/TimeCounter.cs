using System;
using UnityEngine;

namespace Remagures.Tools
{
    public sealed class TimeCounter
    {
        private readonly IStorage _storage;

        public TimeCounter(IStorage storage)
            => _storage = storage ?? throw new ArgumentNullException(nameof(storage));

        public int GetTimeDifference(string key)
        {
            var oldDate = _storage.Load<DateTime>(key);
            var difference = DateTime.Now.Subtract(oldDate);
            return (int)difference.TotalSeconds;
        }
    
        public void SaveCurrentTime(string key)
            => _storage.Save(DateTime.Now, key);
    }
}
