using Remagures.SO;
using Remagures.Time;
using UnityEngine;

namespace Remagures.Notifications
{
    public class MeatNotificationComponent : NotificationComponent
    {
        [Header("Meat Stuff")]
        [SerializeField] private FloatValue _rawCount;
        [SerializeField] private TimeCounter _timeCounter;

        public static MeatNotificationComponent Instance { get; private set; }

        protected override void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    
        public void Init()
        {
            _canNotify = true;
            _delay = 300 - _timeCounter.CheckDate("MeatTime") + ((int)_rawCount.Value - 1) * 300;
        
            if (_rawCount.Value <= 0 || _delay <= 0)
                _canNotify = false;
        
            SendNotification(1);
        }
    }
}
