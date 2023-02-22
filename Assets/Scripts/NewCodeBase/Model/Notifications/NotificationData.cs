using System;
using Remagures.Tools;

namespace Remagures.Model.Notifications
{
    public readonly struct NotificationData
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string SmallIconName;
        public readonly int ID;

        public NotificationData(string title, string description, string smallIconName, int id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SmallIconName = smallIconName ?? throw new ArgumentNullException(nameof(smallIconName));
            ID = id.ThrowExceptionIfLessThanZero();
        }
    }
}