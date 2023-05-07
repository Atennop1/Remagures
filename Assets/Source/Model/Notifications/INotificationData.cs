namespace Remagures.Model.Notifications
{
    public interface INotificationData
    {
        string Title { get; }
        string Description { get; }
        string SmallIconName { get; }
        int ID { get; }
    }
}