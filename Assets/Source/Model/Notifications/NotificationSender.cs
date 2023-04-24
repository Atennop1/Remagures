using Unity.Notifications.Android;

namespace Remagures.Model.Notifications
{
    public sealed class NotificationSender
    {
        public NotificationSender() 
        {
            var channel = new AndroidNotificationChannel
            {
                Name = "Notifications",
                Description = "Channel for in-game notifications",
                Id = "news",
                Importance = Importance.Default
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    
        public void Send(NotificationData notificationData, float delay)
        {
            var notification = new AndroidNotification
            {
                Title = notificationData.Title,
                Text = notificationData.Description,
                SmallIcon = notificationData.SmallIconName,
                FireTime = System.DateTime.Now.AddSeconds(delay)
            };
            
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "news", notificationData.ID);
        }
    }
}
