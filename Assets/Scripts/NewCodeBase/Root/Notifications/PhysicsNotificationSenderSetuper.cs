using System;
using Remagures.Model.Notifications;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsNotificationSenderSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private INotificationSenderFactory _notificationSenderFactory;
        [SerializeField] private PhysicsNotificationSender _physicsNotificationSender;

        private void Awake()
            => _physicsNotificationSender.Construct(_notificationSenderFactory.Create());
    }
}