using System;
using Remagures.Model.MeatSystem;
using Remagures.Tools;
using SaveSystem;
using SaveSystem.Paths;

namespace Remagures.Model.Notifications
{
    public sealed class MeatCookerWithNotifications : IMeatCooker, INotificationSender
    {
        public int RawMeatCount => _meatCooker.RawMeatCount;
        
        private readonly IMeatCooker _meatCooker;
        private readonly NotificationData _notificationData;
        
        private readonly TimeDifferenceCounter _timeDifferenceCounter = new(new BinaryStorage<DateTime>(new Path("RemainingMeatCookingTime")));
        private readonly NotificationSender _notificationSender = new();

        public MeatCookerWithNotifications(IMeatCooker meatCooker, NotificationData notificationData)
        {
            _meatCooker = meatCooker ?? throw new ArgumentNullException(nameof(meatCooker));
            _notificationData = notificationData;
        }

        public void OnContinueGame()
            => Unity.Notifications.Android.AndroidNotificationCenter.CancelDisplayedNotification(_notificationData.ID);
        
        private void Send()
        {
            Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(_notificationData.ID);
            var delay = 300 - _timeDifferenceCounter.GetTimeDifference() + (_meatCooker.RawMeatCount - 1) * 300;

            if (delay > 0)
                _notificationSender.Send(_notificationData, delay);
        }

        public void CookMeat(int count)
            => _meatCooker.CookMeat(count);

        public void AddRawMeat()
        {
            _meatCooker.AddRawMeat();
            Send();
        }
        
        public void OnPauseGame() { }
    }
}
