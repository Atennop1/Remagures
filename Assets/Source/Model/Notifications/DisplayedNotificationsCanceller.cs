using Unity.Notifications.Android;
using UnityEngine;

namespace Remagures.Model.Notifications
{
    public sealed class DisplayedNotificationsCanceller : MonoBehaviour
    {
        private void OnApplicationFocus(bool focusStatus) 
            => AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }
}