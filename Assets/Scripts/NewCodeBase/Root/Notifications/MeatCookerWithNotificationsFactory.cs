using Remagures.Model.MeatSystem;
using Remagures.Model.Notifications;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MeatCookerWithNotificationsFactory : SerializedMonoBehaviour, IMeatCookerFactory, INotificationSenderFactory
    {
        [SerializeField] private IMeatCookerFactory _meatCookerFactory;
        [SerializeField] private NotificationDataFactory _notificationDataFactory;

        IMeatCooker IMeatCookerFactory.Create()
            => new MeatCookerWithNotifications(_meatCookerFactory.Create(), _notificationDataFactory.Create());
        
        public INotificationSender Create()
            => new MeatCookerWithNotifications(_meatCookerFactory.Create(), _notificationDataFactory.Create());
    }
}