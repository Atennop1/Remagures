using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "NotificationData", menuName = "Notifications/NotificationData")]
    public sealed class NotificationData : ScriptableObject
    {
        [SerializeField] public string Title;
        [SerializeField] public string Description;
        [SerializeField] public string SmallIconName;
        [SerializeField] public int ID;
    }
}