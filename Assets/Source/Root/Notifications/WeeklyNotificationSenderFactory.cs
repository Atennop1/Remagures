using Remagures.Model.Notifications;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class WeeklyNotificationSenderFactory : SerializedMonoBehaviour, INotificationSenderFactory
    {
        [SerializeField] private NotificationDataFactory _notificationDataFactory;
        
        public INotificationSender Create()
            => new WeeklyNotificationSender(_notificationDataFactory.Create());
    }
}