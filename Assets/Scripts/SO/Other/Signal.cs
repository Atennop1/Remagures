using System.Collections.Generic;
using UnityEngine;

namespace Remagures.SO.Other
{
    [CreateAssetMenu(fileName = "New Signal", menuName = "Other/Signal")]
    public class Signal : ScriptableObject
    {
        [SerializeField] private List<SignalListener> _listeners = new();

        public void Invoke()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnSignalRaised();
        }

        public void RegisterListener(SignalListener listener)
        {
            _listeners.Add(listener);
        }

        public void DeRegisterListener(SignalListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
