using System;
using Remagures.Tools;

namespace Remagures.Model.Notifications
{
    public struct NotificationData : INotificationData
    {
        public string Title { get; }
        public string Description { get; }
        public string SmallIconName { get; }
        public int ID { get; }

        public NotificationData(string title, string description, string smallIconName, int id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SmallIconName = smallIconName ?? throw new ArgumentNullException(nameof(smallIconName));
            ID = id.ThrowExceptionIfLessThanZero();
        }
    }
}