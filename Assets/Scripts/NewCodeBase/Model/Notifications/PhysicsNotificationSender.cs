using System;
using UnityEngine;

namespace Remagures.Model.Notifications
{
    public sealed class PhysicsNotificationSender : MonoBehaviour
    {
        private INotificationSender _notificationSender;

        public void Construct(INotificationSender notificationSender)
            => _notificationSender = notificationSender ?? throw new ArgumentNullException(nameof(notificationSender));

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _notificationSender.OnContinueGame();
            }
            else
            {
                _notificationSender.OnPauseGame();
            }
        }
    }
}