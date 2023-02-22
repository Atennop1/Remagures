using Remagures.Model.MeatSystem;
using Remagures.Root;
using Remagures.Tools;

namespace Remagures.Model.Notifications
{
    public sealed class MeatNotificationSender : INotificationSender, IUpdatable
    {
        private readonly NotificationSender _notificationSender = new();
        private readonly MeatCooker _meatCooker;
        private readonly TimeCounter _timeCounter;
        private readonly NotificationData _notificationData;

        public MeatNotificationSender(NotificationData notificationData)
            => _notificationData = notificationData;

        public void OnContinueGame()
            => Unity.Notifications.Android.AndroidNotificationCenter.CancelDisplayedNotification(_notificationData.ID);
        
        public void Update()
        {
            if (_meatCooker.HasRawMeatAdded)
                Send();
        }

        private void Send()
        {
            Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(_notificationData.ID);
            var delay = 300 - _timeCounter.GetTimeDifference("MeatTime") + (_meatCooker.RawMeatCount - 1) * 300;

            if (delay > 0)
                _notificationSender.Send(_notificationData, delay);
        }
        
        public void OnPauseGame() { }
    }
}
