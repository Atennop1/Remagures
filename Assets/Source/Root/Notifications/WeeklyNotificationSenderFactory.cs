using Remagures.Model.Notifications;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class WeeklyNotificationSenderFactory : SerializedMonoBehaviour, INotificationSenderFactory
    {
        [SerializeField] private INotificationData _notificationData;
        private INotificationSender _builtSender;
        
        public INotificationSender Create()
        {
            if (_builtSender != null)
                return _builtSender;
            
            _builtSender = new WeeklyNotificationSender(_notificationData);
            return _builtSender;
        }
    }
}