using Unity.Notifications.Android;

namespace Remagures.Model.Notifications
{
    public sealed class WeeklyNotificationSender : INotificationSender
    {
        private readonly NotificationSender _notificationSender = new();
        private readonly NotificationData _notificationData;
        
        private const int SECONDS_IN_WEEK = 3600 * 24 * 7;

        public WeeklyNotificationSender(NotificationData notificationData)
            => _notificationData = notificationData;

        public void OnPauseGame()
            => _notificationSender.Send(_notificationData, SECONDS_IN_WEEK);

        public void OnContinueGame()
            => AndroidNotificationCenter.CancelNotification(_notificationData.ID);
    }
}