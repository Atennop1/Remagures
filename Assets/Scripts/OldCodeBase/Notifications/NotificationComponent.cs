using Unity.Notifications.Android;
using UnityEngine;

namespace Remagures.Notifications
{
    public class NotificationComponent : MonoBehaviour
    {
        protected float _delay;
        protected bool _canNotify;

        [Header("Notification Settings")]
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private string _smallIcon;

        protected virtual void Awake() 
        {
            var channel = new AndroidNotificationChannel()
            {
                Name = "Notifications",
                Description = "Channel for in-game notifications",
                Id = "news",
                Importance = Importance.Default
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    
        protected void SendNotification(int id)
        {
            if (!_canNotify) return;
        
            var notification = new AndroidNotification()
            {
                Title = _title,
                Text = _description,
                SmallIcon = _smallIcon,
                FireTime = System.DateTime.Now.AddSeconds(_delay),
            };
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "news", id);
        }

        protected virtual void OnApplicationFocus(bool focusStatus) 
            => AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }
}
