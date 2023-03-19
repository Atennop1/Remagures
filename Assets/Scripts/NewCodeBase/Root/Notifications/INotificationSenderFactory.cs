using Remagures.Model.Notifications;

namespace Remagures.Root
{
    public interface INotificationSenderFactory
    {
        INotificationSender Create();
    }
}