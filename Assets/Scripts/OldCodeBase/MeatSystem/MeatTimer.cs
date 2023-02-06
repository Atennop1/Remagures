using Remagures.Time;
using UnityEngine;

namespace Remagures.MeatSystem
{
    public class MeatTimer : MonoBehaviour
    {
        [SerializeField] private TimeCounter _timeCounter;
        [SerializeField] private MeatView _view;

        public float Timer { get; private set; }

        private void OnEnable()
        {
            Timer = 300;

            if (_view.RawCount.Value > 0)
                Timer -= _timeCounter.CheckDate("MeatTime");
            
            _view.UpdateMeat();
        }

        private void FixedUpdate()
        {
            while (Timer < 0 && _view.RawCount.Value > 0)
            {
                switch (Timer)
                {
                    case <= 0 and > -300:
                        Timer = 300 + Timer;
                        _timeCounter.SaveDate("MeatTime");
                        break;
                    
                    case < -300:
                        Timer += 300;
                        break;
                }
                
                _view.RawCount.Value--;
                _view.CookedCount.Value++;
            }

            if (_view.RawCount.Value > 0) Timer -= UnityEngine.Time.deltaTime;
            else _timeCounter.SaveDate("MeatTime");

            _view.UpdateTimer();
            _view.UpdateMeat();
        }
    }
}
