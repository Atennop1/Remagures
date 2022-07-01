using UnityEngine;
using Unity.Notifications.Android;

public class NotificationComponent : MonoBehaviour
{
    protected float _delay;
    protected bool _canNotify;

    [Header("Notification Settings")]
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private string _smallIcon;
    
    protected bool IsFocus { get; private set; }

    public virtual void Init() { }

    protected virtual void Awake() 
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "Notificatios",
            Description = "Channel for in-game notifications",
            Id = "news",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void FixedUpdate() 
    {
        if (IsFocus)
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }
    
    public void SendNotification(int id)
    {
        if (_canNotify)
        {
            AndroidNotification notification = new AndroidNotification()
            {
                Title = this._title,
                Text = this._description,
                SmallIcon = this._smallIcon,
                FireTime = System.DateTime.Now.AddSeconds(_delay),
            };
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "news", id);
        }
    }

    protected virtual void OnApplicationFocus(bool focusStatus) 
    {
        IsFocus = true;
    }

    protected virtual void OnApplicationPause(bool pauseStatus) 
    {
        IsFocus = false;
    }
}
