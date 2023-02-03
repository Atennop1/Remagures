using UnityEngine;
using UnityEngine.Events;

namespace Remagures.SO
{
    public class SignalListener : MonoBehaviour
    {
        [SerializeField] private Signal _signal;
        [SerializeField] private UnityEvent _signalEvent;
    
        public void OnSignalRaised()
        {
            _signalEvent.Invoke();
        }

        private void OnEnable()
        {
            _signal.RegisterListener(this);
        }

        private void OnDisable()
        {
            _signal.DeRegisterListener(this);
        }
    }
}
