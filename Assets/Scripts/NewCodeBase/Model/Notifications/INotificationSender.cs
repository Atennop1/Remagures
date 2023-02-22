namespace Remagures.Model.Notifications
{
    public interface INotificationSender
    {
        void OnPauseGame();
        void OnContinueGame();
    }
}