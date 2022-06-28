using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Signal", menuName = "Other/Signal")]
public class Signal : ScriptableObject
{
    private List<SignalListener> _listeners = new List<SignalListener>();
    public IReadOnlyList<SignalListener> Listeners => _listeners;

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
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
