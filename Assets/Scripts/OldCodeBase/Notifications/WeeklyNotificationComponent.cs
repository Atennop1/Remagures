using Unity.Notifications.Android;

namespace Remagures.Notifications
{
    public class WeeklyNotificationComponent : NotificationComponent
    {
        private static WeeklyNotificationComponent Instance { get; set; }

        protected override void Awake() 
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        private void Start() 
        {
            AndroidNotificationCenter.CancelNotification(0);
            _canNotify = true;
        }
    
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                base.OnApplicationFocus(true);
                _delay = 3600 * 24 * 7;
                SendNotification(0);
            }
            else
            {
                Start();
            }
        }
    }
}