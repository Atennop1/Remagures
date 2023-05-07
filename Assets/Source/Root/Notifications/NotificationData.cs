using Remagures.Model.Notifications;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "NotificationData", menuName = "Notifications/NotificationData")]
    public sealed class NotificationData : ScriptableObject, INotificationData
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public string SmallIconName { get; private set; }
        [field: SerializeField] public int ID { get; private set; }
    }
}