using Remagures.Model.MeatSystem;
using Remagures.Model.Notifications;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MeatCookerWithNotificationsFactory : SerializedMonoBehaviour, IMeatCookerFactory, INotificationSenderFactory
    {
        [SerializeField] private IMeatCookerFactory _meatCookerFactory;
        [SerializeField] private INotificationData _notificationData;
        private MeatCookerWithNotifications _builtMeatCooker;

        IMeatCooker IMeatCookerFactory.Create()
            => Create();

        INotificationSender INotificationSenderFactory.Create()
            => Create();

        private MeatCookerWithNotifications Create()
        {
            if (_builtMeatCooker != null)
                return _builtMeatCooker;
            
            _builtMeatCooker = new MeatCookerWithNotifications(_meatCookerFactory.Create(), _notificationData);
            return _builtMeatCooker;
        }
    }
}