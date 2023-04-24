using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class NotificationDataFactory : SerializedMonoBehaviour
    {
        [SerializeField] private NotificationData _data;

        public Model.Notifications.NotificationData Create()
            => new(_data.Title, _data.Description, _data.SmallIconName, _data.ID);
    }
}